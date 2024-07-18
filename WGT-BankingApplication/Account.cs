namespace WGT_BankingApplication;

abstract class Account : Customer
{
    private string _accountNumber;
    private decimal _balance = 0.00m;

    public string AccountNumber { get => _accountNumber; }
    public virtual decimal Balance { get => _balance; protected set => _balance = value; }

    public Account(int ID, string FirstName, string Surname, string Password) : base(ID, FirstName, Surname, Password)
    {
        _accountNumber = GenerateRandomAccountNumber();
    }

    public abstract void OpenAccount();
    public abstract void CloseAccount();
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);

    private string GenerateRandomAccountNumber()  // Rough implementation of generating a unique account number for each account
    {
        string randomNumber = "";
        Random rand = new Random();
        for (int i = 0; i < 8; i++)
        {
            randomNumber += rand.Next(0, 9);
        }
        return randomNumber;
    }
}
