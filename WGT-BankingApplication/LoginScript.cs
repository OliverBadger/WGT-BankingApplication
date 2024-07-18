using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGT_BankingApplication
{
    internal class LoginScript
    {

        private Customer[] _customers;

        public Customer[] Customers { get => _customers; set => _customers = value; }


        //do while for login if credentials are valid escape do while call next method outside of do while block

        //We can move login methods and customer searching methods to dedicated classes for clarity and to keep this class consice
        public void login()
        {
            string username = "";

            bool CorrectUsernameProvided = false;

            do
            {
                Console.Clear();
                //encryption testing (should make unit test for this)
                //string test = Encryption.encryptString("poapsdopasodasf");
                //string test2 = Encryption.Decode(test);
                //Console.WriteLine(test);
                //Console.WriteLine(test2);

                Console.Write("Please Enter Employee Username: ");
                string? employeeUserName = Console.ReadLine();
                // Authenticates if teller username is correct
                if (isUserNameValid(employeeUserName))
                {
                    CorrectUsernameProvided = true;
                    username = employeeUserName;
                }

            } while (!CorrectUsernameProvided);

            // Gets password

            bool CorrectPasswordProvided = false;

            do
            {
                Console.Clear();
                Console.Write("Please Enter Password: ");
                string? employeePassword = Console.ReadLine();
                if (isPasswordValid(username, employeePassword))
                    CorrectPasswordProvided = true;

            } while (!CorrectPasswordProvided);

        }
        public bool isUserNameValid(string username)
        {
            if (UserCredentials.employeeCredentials.ContainsKey(Encryption.encryptString(username)))
            {
                return true;
            }
            return false;
        }

        //Script for encoding run after password checks
        public bool isPasswordValid(string username, string password)
        {
            if (Encryption.Decode(UserCredentials.employeeCredentials[Encryption.encryptString(username)]) == password)
            {
                return true;
            }
            return false;
        }


        //Only called after teller has succesfully logged in and credentials match
        public void InputLoop()
        {
            Console.Clear();
            //testing 
            Console.WriteLine("1. Customer search using ID");
            Console.WriteLine("2. Customer search using full name (not implemented can return more than one customer if same surname)");
            Console.WriteLine("3. Customer search using account number (not implemented yet)");

            bool validSelection = false;
            do
            {
                string? input = Console.ReadLine();
                switch (input) //have key of possible inputs and dict of output
                {
                    case "1":
                        validSelection = true;
                        SearchByID();
                        break;
                    case "2":
                        validSelection = true;
                        SearchBySurname();
                        break;
                    default: break;
                }

            } while (!validSelection);
        }

        public void SearchByID()
        {
            bool MatchFound = false;
            do
            {


                Console.Clear();
                Console.Write("Please enter customer id: ");
                string? id = Console.ReadLine();

                foreach (Customer c in _customers)
                {
                    if (c.ID.ToString() == id)
                    {
                        Console.WriteLine($"Account {id} Found ");
                        LookAtCustomerDetails(c);
                        MatchFound = true;
                    }
                }
            } while (!MatchFound);
        }

        public void SearchBySurname()
        {
            bool MatchFound = false;
            do
            {
                Console.Clear();
                Console.Write("Please enter customer full name: "); // this can return more than one /\
                string? fullName = Console.ReadLine();

                foreach (Customer c in _customers)
                {
                    string CustomerName = $"{c.FirstName} {c.Surname}".ToLower();

                    if (CustomerName == fullName.ToLower())
                    {
                        LookAtCustomerDetails(c);
                        MatchFound = true;
                    }
                }
            } while (!MatchFound);
        }

        //Before calling we sould add some security stuff to make sure the customer has proven their identity 
        //TODO add account information and ability to look at individual accounts here
        public void LookAtCustomerDetails(Customer customer)
        {
            Console.Clear();
            Console.WriteLine($""""
            ID: {customer.ID}
            name: {customer.FirstName} {customer.Surname}
            """");
        }
    }
}
