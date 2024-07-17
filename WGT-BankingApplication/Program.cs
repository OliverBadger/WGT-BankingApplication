
using Newtonsoft.Json;
using System;

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

        Console.ReadKey();
    }

    //this can be moved elsewhere
    private static void initUsers()
    {
        if (File.Exists("Users.json"))
        {
            //Read in data
            string json = File.ReadAllText("Users.json");
            Customer[] Customers = JsonConvert.DeserializeObject<Customer[]>(json);
        }
        else
        {
            //generate new users 
            CustomerGenerator.GenerateNewUserData();
        }
    }
}
