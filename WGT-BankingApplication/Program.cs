
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

        //changing to use serialized accounts 
        

        Customer c1 = Customers[0];
        Customer c2 = Customers[1];
        Customer c3 = Customers[2];
        Customer c4 = Customers[3];

        Console.WriteLine(Customers[1].FirstName);

        ISA s1 = new(c1, 500);
        ISA s2 = new(c2, 7750);
        ISA s3 = new(c3, 4340);

        PersonalAccount p1 = new(c1, 100);
        PersonalAccount p2 = new(c2, 900);

        // Will need to make sure initial deposit entered is greater than 120, as the annual fee will instantly deduct if the business account is new/being created today.
        BusinessAccount b1 = new(c1, "OpenAI", "NonProfit", "3180 18th St. San Francisco, California 94110", 140);  
        BusinessAccount b2 = new(c2, "Meta", "ForProfit", "1 Meta Way Menlo Park California 94025", 10000);
        BusinessAccount b3 = new(c3, "HP", "ForProfit", "70 St Mary Axe London EC3A 8BE", 35350, DateTime.Parse("18/07/2021"), DateTime.Parse("18/07/2024"));  // Test for reading in a file

        // This can be removed, just testing to see if the accounts are working
        Console.WriteLine($"""
            {c1.FirstName} has a balance of: {b1.Balance:C} in their business account.
            The last time {c1.FirstName} was charged was {b1.LastAnnualChargeDate}

            {c2.FirstName} has a balance of: {b2.Balance:C} in their business account.
            The last time {c2.FirstName} was charged was {b2.LastAnnualChargeDate}

            {c3.FirstName} has a balance of: {b3.Balance:C} in their business account.
            The last time {c3.FirstName} was charged was {b3.LastAnnualChargeDate}

            """);

        Console.WriteLine($"{c1.FirstName} has a balance of: {p1.Balance:C} in their personal account.");
        Console.WriteLine($"{c2.FirstName} has a balance of: {p2.Balance:C} in their personal account.");
        Console.WriteLine();
        Console.WriteLine($"{c1.FirstName} has a balance of: {s1.Balance:C} in their ISA account.");
        Console.WriteLine($"{c2.FirstName} has a balance of: {s2.Balance:C} in their ISA account.");

        //Console.WriteLine("\nThese accounts are associated to Customer 1:");
        //foreach (var test in c1.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance}");
        //}

        //Console.WriteLine("\nThese accounts are associated to Customer 2:");
        //foreach (var test in c2.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance}");
        //}

        //Console.WriteLine("\nThese accounts are associated to Customer 3:");
        //foreach (var test in c3.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance}");
        //}

        //login();
        //InputLoop();
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
            Customers = CustomerGenerator.GenerateNewUserData();
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
        bool MatchFound = false;
        do
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
                    MatchFound = true;
                }
            }
        } while (!MatchFound);
    }

    private static void SearchBySurname()
    {
        bool MatchFound = false;
        do
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
                    MatchFound=true;
                }
            }
        } while (!MatchFound);
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
