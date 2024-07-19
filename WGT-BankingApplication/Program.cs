
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

        //Login script validates users
        LoginScript ls = new LoginScript();

        //changing to use serialized accounts 
        Customer c1 = Customers[0];
        Customer c2 = Customers[1];
        Customer c3 = Customers[2];
        Customer c4 = Customers[3];

        // Console.WriteLine(Customers[1].FirstName);
      
        ISA s1 = new(c1, 500);
        ISA s2 = new(c2, 7750);
        ISA s3 = new(c3, 4340);

        PersonalAccount p1 = new(c1, 100);
        PersonalAccount p2 = new(c2, 900);

        // Will need to make sure initial deposit entered is greater than 120, as the annual fee will instantly deduct if the business account is new/being created today.
        BusinessAccount b1 = new(c1, "OpenAI", "NonProfit", "3180 18th St. San Francisco, California 94110", 140, 1000, false);  
        BusinessAccount b2 = new(c2, "Meta", "ForProfit", "1 Meta Way Menlo Park California 94025", 10000, 0, true);
        BusinessAccount b3 = new(c3, "HP", "ForProfit", "70 St Mary Axe London EC3A 8BE", 35350, 600, false, DateTime.Parse("18/07/2021"), DateTime.Parse("18/07/2024"));  // Test for reading in a file

        // This can be removed, just testing to see if the accounts are working
        //Console.WriteLine($"""
        //    {c1.FirstName} has a balance of: {b1.Balance:C} in their business account.
        //    The last time {c1.FirstName} was charged was {b1.LastAnnualChargeDate}

        //    {c2.FirstName} has a balance of: {b2.Balance:C} in their business account.
        //    The last time {c2.FirstName} was charged was {b2.LastAnnualChargeDate}

        //    {c3.FirstName} has a balance of: {b3.Balance:C} in their business account.
        //    The last time {c3.FirstName} was charged was {b3.LastAnnualChargeDate}

        //    """);

        //Console.WriteLine($"{c1.FirstName} has a balance of: {p1.Balance:C} in their personal account.");
        //Console.WriteLine($"{c2.FirstName} has a balance of: {p2.Balance:C} in their personal account.");
        //Console.WriteLine();
        //Console.WriteLine($"{c1.FirstName} has a balance of: {s1.Balance:C} in their ISA account.");
        //Console.WriteLine($"{c2.FirstName} has a balance of: {s2.Balance:C} in their ISA account.");

        //Console.WriteLine("\nThese accounts are associated to Customer 1:");
        //foreach (var test in c1.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance:C}");
        //}

        //Console.WriteLine("\nThese accounts are associated to Customer 2:");
        //foreach (var test in c2.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance:C}");
        //}

        //Console.WriteLine("\nThese accounts are associated to Customer 3:");
        //foreach (var test in c3.Accounts)
        //{
        //    Console.WriteLine($"Account: {test}, Balance: {test.Balance:C}");
        //}

        // Writes the array of customers to the Login Script
        //ls.Customers = Customers;
        //ls.login();
        //ls.InputLoop();
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
            CustomerGenerator newCustomers = new();
            Customers = newCustomers.GenerateNewUserData();
            //generate new accounts => 
        }
    }
}
