
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Formats.Tar;
using System.Globalization;
using System.Runtime.InteropServices;

namespace WGT_BankingApplication;

class MyClass{
    public static void Main()
    {
        initUsers();
        UserCredentials.initUserCredentials();
        //Console.WriteLine("This is a Banking Application!");
        //Console.WriteLine("This is just to test the branches");

        //changing to use serialized accounts 
        Customer testcustomer = Customers[0];

        Customer c1 = new Customer(1, "Bob","Beltch" , "test1234");
        Customer c2 = new Customer(1, "Bob","Dylan" , "test2234");
        Customer c3 = new Customer(1, "Bob","Marley" , "test3234");
        Customer c4 = new Customer(1, "Bob","Ross" , "test4234");
        
        ISA savAcc1 = new ISA(c1, 1);
        c1.AddAccount(savAcc1);

        ISA savAcc2 = new ISA(c1, 1);
        ISA savAcc3 = new ISA(c1, 1);
        ISA savAcc4 = new ISA(c1, 1);
        
        PersonalAccount p1 = new PersonalAccount();
        c1.AddAccount(p1);

        //Console.WriteLine(c1.FirstName);
        //Console.WriteLine(c1.ID);
        //Console.WriteLine(c1.Account_List);
        //Console.WriteLine(c1.GetAccounts);
        
        Console.WriteLine($"Welcome back {c1.FirstName}, your account number is: {p1.AccountNumber}");
        Console.WriteLine($"Your current balance is: {p1.Balance}");
        
        login();
        InputLoop();
    }

    static Customer[] Customers;
    //this can be moved elsewhere
    private static void initUsers()
    {
        if (File.Exists("Users.json"))
        {
            //Read in data
            string json = File.ReadAllText("Users.json");
            Customers = JsonConvert.DeserializeObject<Customer[]>(json);
        }
        else
        {
            //generate new users 
            CustomerGenerator.GenerateNewUserData();
            //generate new accounts => 
        }
    }


    //do while for login if credentials are valid escape do while call next method outside of do while block


    //We can move login methods and customer searching methods to dedicated classes for clarity and to keep this class consice
    private static void login()
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

            Console.Write("Please Enter Employee Id: ");
            string? employeeUserName = Console.ReadLine();
            if (isUserNameValid(employeeUserName))
            {
                CorrectUsernameProvided = true;
                username = employeeUserName;
            }
                

        } while (!CorrectUsernameProvided);

        //get password

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
    private static bool isUserNameValid(string username)
    {
        if (UserCredentials.employeeCredentials.ContainsKey(Encryption.encryptString(username)))
        {
            return true;
        }
        return false;
    }

    private static bool isPasswordValid(string username, string password)
    {
        if (Encryption.Decode(UserCredentials.employeeCredentials[Encryption.encryptString(username)]) == password)
        {
            return true;
        }
        return false;
    }


    //Only called after teller has succesfully logged in and credentials match
    private static void InputLoop()
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

    private static void SearchByID()
    {
        Console.Clear();
        Console.Write("Please enter customer id: ");
        string? id = Console.ReadLine();
        
        foreach (Customer c in Customers)
        {
            if (c.ID.ToString() == id)
            {
                Console.WriteLine($"Account {id} Found ");
                LookAtCustomerDetails(c);
            }
        }
    }

    private static void SearchBySurname()
    {
        Console.Clear();
        Console.Write("Please enter customer full name: "); // this can return more than one /\
        string? fullName = Console.ReadLine();

        foreach (Customer c in Customers)
        {
            string CustomerName = $"{c.FirstName} {c.Surname}".ToLower();

            if (CustomerName == fullName.ToLower())
            {
                LookAtCustomerDetails(c);
            }
        }
    }

    //Before calling we sould add some security stuff to make sure the customer has proven their identity 
    //TODO add account information and ability to look at individual accounts here
    private static void LookAtCustomerDetails(Customer customer)
    {
        Console.Clear();
        Console.WriteLine($""""
            ID: {customer.ID}
            name: {customer.FirstName} {customer.Surname}
            """");
    }
}
