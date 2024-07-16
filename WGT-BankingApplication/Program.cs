namespace WGT_BankingApplication;

class MyClass{
    public static void Main()
    {
        //Console.WriteLine("This is a Banking Application!");
        //Console.WriteLine("This is just to test the branches");

        Customer c1 = new Customer(1, "Bob", "test3245");
        Console.WriteLine(c1.Name);
        Console.WriteLine(c1.ID);
        Console.WriteLine(c1.Account_List);
        // Console.WriteLine(c1.GetAccounts);

        PersonalAccount p1 = new PersonalAccount();
        Console.WriteLine($"Welcome back {c1.Name}, your account number is: {p1.AccountNumber}");
        Console.WriteLine($"Your current balance is: {p1.Balance}");

        Console.ReadKey();
    }
}