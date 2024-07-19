namespace WGT_BankingApplication;

abstract class Account : Customer
{
     
    private string _accountNumber; // Stores the account number
    private decimal _balance = 0.00m; // Stores the account balance, initialised to 0.00

    // Property for account number to be read-only
    public string AccountNumber { get => _accountNumber; }
    // Property to get and set the balance, balance can only be set within the class or derived classes
    public virtual decimal Balance { get => _balance; protected set => _balance = value; }

    // Constructor to initialise an account with basic details and generate a unique account number
    public Account(int ID, string FirstName, string Surname, string Password, string CustomerNumber) : base(ID, FirstName, Surname, Password, CustomerNumber)
    {
        _accountNumber = GenerateRandomAccountNumber();
    }

    // Abstract methods to be implemented by derived classes for account operations
    public abstract void OpenAccount();
    public abstract void CloseAccount();
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);

    // Method to generate a random 8-digit number
    private string GenerateRandomAccountNumber()  // Rough implementation of generating a unique account number for each account
    {
        string randomNumber = "";
        Random rand = new Random();
        // Append a random digit to the account number
        for (int i = 0; i < 8; i++)
        {
            randomNumber += rand.Next(0, 9);
        }
        return randomNumber; // Return the generated account number
    }
}
