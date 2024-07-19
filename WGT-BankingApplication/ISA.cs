namespace WGT_BankingApplication;

class ISA : Account
{
    private Customer _accountHolder; //Stores the customer who holds the account
    private decimal APR = 2.75m;//annual percentage rate is set at 2.75%
    private int _accountNumber;//account number is generated upon creation
    private decimal _annualInterestAccured;//tracks the annual interest accrued
    private DateTime _createdDate;//date and time when the account was created
    private bool _isActive;//indicates if the account is still active or closed
    private int _monthMaturity;//Tracks the number of months the account has matured
    private int _yearMaturity;//tracks the number of years the account has matured
    private List<decimal> _receipts = new List<decimal>();//a list to keep track of account balance over time

    //Takes a customer object and an initial deposit amount
    //Initializes various properties, sets the account as active, and adds the initial balance to receipts
    //Calls method to generate a random account number
    //adds this account to the customers list of accounts
    public ISA(Customer Customer, int InitialDeposit) : base(Customer.ID, Customer.FirstName, Customer.Surname, Customer.Password, Customer.CustomerNumber)
    {
        _accountHolder = Customer;
        _accountNumber = GenerateAccountNumber();
        Balance = InitialDeposit;
        _createdDate = DateTime.Now;
        _isActive = true;
        _receipts.Add(Balance);
        Customer.AddAccount(this);
    }

    //generates a random 8 digit number
    private int GenerateAccountNumber()
    {
        return new Random().Next(10000000, 99999999); // Example
    }

    //Sets the account status to active
    public override void OpenAccount()
    {
        _isActive = true;
    }

    //sets the account status to inactive
    public override void CloseAccount()
    {
        _isActive = false;
    }

    //adds a specified amount to account balance
    //throws an exception if the deposit amount is <= 0
    public override void Deposit(decimal amount)
    {
        if (amount <= 0) { throw new ArgumentException("Deposit amount must be greater than 0."); }
        Balance += amount;
        _receipts.Add(Balance);
    }

    //Subtracts a specified amount from acc balance and throws an exception if the withdrawal amount is <= 0 and if withdrawal has insufficient funds
    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
        if (Balance - amount < 0) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        _receipts.Add(Balance);

    }

    //checks if the account has matured for one year
    public void CalculateAnnualInterest()
    {
        if (HasMaturedOneYear())
        {
            decimal averageAnnualBalance = 0m;

            //Calculate Average Annual Balance
            foreach (int x in _receipts)
            {
                averageAnnualBalance += _receipts.IndexOf(x);
            }

            decimal interest = averageAnnualBalance * (APR / 100);
            Balance += interest;
            _annualInterestAccured += interest;
        }
    }

    //returns a string with the account details 

    public string GetAccountDetails()
    {
        return $"Account Number: {_accountNumber}, Balance: {Balance:C}, Annual Interest Rate: {APR}%, Created Date: {_createdDate}, Active: {_isActive}";
    }

    //checks if account has matured one year
    public bool HasMaturedOneYear()
    {
        return DateTime.Now >= _createdDate.AddYears(1);
    }


    //checks if account has matured in a month
    // Not tested
    public bool HasMaturedOneMonth()
    {
        int difference = 0;
        while (DateTime.Now >= _createdDate.AddMonths(1)) { difference++; }
        if (difference <= _monthMaturity)
        {
            _monthMaturity = difference;
            return true;
        }
        else { return false; }
    }
}
