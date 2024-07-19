using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WGT_BankingApplication
{
    internal class LoginScript
    {

        private Customer[] _customers;
        private static string userDetails = "Users.JSON";

        public Customer[] Customers { get => _customers; set => _customers = value; }


        //do while for login if credentials are valid escape do while call next method outside of do while block

        //We can move login methods and customer searching methods to dedicated classes for clarity and to keep this class consice
        public void login()
        //this is the main login method
        {
            string username = "";

            bool CorrectUsernameProvided = false;

            //lopp until a username is provided
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
            //loops until correct passw is provided
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

        //checks if username is valid
        public bool isUserNameValid(string username)
        {
            if (UserCredentials.employeeCredentials.ContainsKey(Encryption.EncryptString(username)))
            {
                return true;
            }
            return false;
        }

        //Script for encoding run after password checks
        public bool isPasswordValid(string username, string password)
        {
            if (Encryption.Decode(UserCredentials.employeeCredentials[Encryption.EncryptString(username)]) == password)
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
            Console.WriteLine("1. Customer search using Customer Number");
            Console.WriteLine("2. Customer search using full name (Please be warned this will not ask for a prompt it will just display the customer details!)");
            Console.WriteLine("3. Customer search using account number (not implemented yet)");

            bool validSelection = false;
            do
            {
                string? input = Console.ReadLine();
                switch (input) //have key of possible inputs and dict of output
                {
                    case "1":
                        validSelection = true;
                        PromptForCustomerNumber();
                        break;
                    case "2":
                        validSelection = true;
                        // Gathers first and last name to search by
                        Console.Clear();
                        Console.Write("Please enter the customer's first name: ");
                        string firstName = Console.ReadLine();
                        Console.Write("Please enter the customer's surname: ");
                        string surname = Console.ReadLine();

                        var potentialCustomers = SearchCustomersByName(firstName, surname);
                        foreach ( var customer in potentialCustomers)
                        {
                            PrintCustomerDetails(customer);
                            Console.WriteLine("");
                            Console.WriteLine(new string('=', 50));
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine(new string('=', 50));
                        }

                        break;
                    default: break;
                }

            } while (!validSelection);
        }

        public void PromptForCustomerNumber()
        {
            // Gathers first and last name to search by
            Console.Clear();
            Console.Write("Please enter the customer's first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter the customer's surname: ");
            string surname = Console.ReadLine();

            var potentialCustomers = SearchCustomersByName(firstName, surname);

            // Incase there are no customers by that name
            if (potentialCustomers.Count == 0)
            {
                Console.WriteLine("No customers found with the given name.");
                return;
            }

            // Saves the random digit position for the prompt 
            Random random = new Random();
            HashSet<int> positions = new HashSet<int>();
            while (positions.Count < 3)
            {
                positions.Add(random.Next(0, 12));
            }

            // Creating a new dictionary to store the location and the character at that location
            Dictionary<int, char> userInputDigits = new Dictionary<int, char>();
            foreach (int pos in positions)
            {
                Console.Write($"Please enter the digit at position {pos + 1}: ");
                char userDigit = Console.ReadKey().KeyChar;
                Console.WriteLine();
                userInputDigits[pos] = userDigit;
            }

            // Validation method to see if it is correct
            foreach (var customer in potentialCustomers)
            {
                if (ValidateCustomerNumber(customer.CustomerNumber, userInputDigits))
                {
                    Console.Clear();
                    PrintCustomerDetails(customer);
                    return;
                }
            }

            Console.WriteLine("Validation failed. No matching customer found.");
        }

        public List<Customer> SearchCustomersByName(string firstName, string surname)
        {
            List<Customer> potentialCustomers = new List<Customer>();
            if (File.Exists(userDetails))
            {
                var existingUsers = JsonConvert.DeserializeObject<Customer[]>(File.ReadAllText(userDetails));
                foreach (var user in existingUsers)
                {
                    if (user.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                        user.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase))
                    {
                        potentialCustomers.Add(user);
                    }
                }
            }
            return potentialCustomers;
        }

        // Checks if the keys are the same as the input
        public bool ValidateCustomerNumber(string customerNumber, Dictionary<int, char> userInputDigits)
        {
            foreach (var entry in userInputDigits)
            {
                if (customerNumber[entry.Key] != entry.Value)
                {
                    return false;
                }
            }
            return true;
        }

        //Before calling we sould add some security stuff to make sure the customer has proven their identity 
        //TODO add account information and ability to look at individual accounts here
        public void PrintCustomerDetails(Customer customer)
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("           Acme Bank Customer Details");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($""""
            ID                : {customer.ID}
            Customer Number   : {customer.CustomerNumber}
            Name              : {customer.FirstName} {customer.Surname}
            """");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Accounts:");
            Console.WriteLine(new string('-', 50));
            if (customer.PersonalAccountList != null)
            {
                foreach (var account in customer.PersonalAccountList)
                {
                    Console.WriteLine($"""
                        Personal Account - Account Number  : {account.AccountNumber}
                        Personal Account - Balance         : {account.Balance}
                        """);
                }
            }
            if (customer.BusinessAccountList != null)
            {
                foreach (var account in customer.BusinessAccountList)
                {
                    Console.WriteLine($"""
                        Business - Account Number  : {account.AccountNumber}
                        Business - Balance         : {account.Balance}
                        """);
                }
            }
            if (customer.Isa != null)
            {
                Console.WriteLine($"""
                        ISA - Account Number  : {customer.Isa.AccountNumber}
                        ISA - Balance         : {customer.Isa.Balance}
                        """);
            }
            Console.WriteLine(new string('=', 50));
        }
    }
}
