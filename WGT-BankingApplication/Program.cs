
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
        
        LoginScript ls = new LoginScript();

        Customer c1 = Customers[0];
        Customer c2 = Customers[1];
        Customer c3 = Customers[2];
        Customer c4 = Customers[3];

        Console.WriteLine(Customers[1].FirstName);


        ISA savAcc1 = new ISA(c1, 1);
        c1.AddAccount(savAcc1);

        ISA savAcc2 = new ISA(c1, 1);
        ISA savAcc3 = new ISA(c1, 1);
        ISA savAcc4 = new ISA(c1, 1);
        
        PersonalAccount p1 = new PersonalAccount();
        c1.AddAccount(p1);
        
        // Writes the array of customers to the Login Script
        ls.Customers = Customers;
        ls.login();
        ls.InputLoop();
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
}
