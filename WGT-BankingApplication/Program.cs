
using Newtonsoft.Json;
using System;
using System.Runtime.InteropServices;

namespace WGT_BankingApplication;

class MyClass{
    public static void Main()
    {
        initUsers();
        //Console.WriteLine("This is a Banking Application!");
        //Console.WriteLine("This is just to test the branches");

        Customer c1 = new Customer(1, "Bob","smith" , "test3245");
        Console.WriteLine(c1.FirstName);
        Console.WriteLine(c1.ID);
        Console.WriteLine(c1.Account_List);
        //Console.WriteLine(c1.GetAccounts);
        
        PersonalAccount p1 = new PersonalAccount();
        Console.WriteLine($"Welcome back {c1.FirstName}, your account number is: {p1.AccountNumber}");
        Console.WriteLine($"Your current balance is: {p1.Balance}");

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
        }
    }

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
