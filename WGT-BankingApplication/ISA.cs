namespace WGT_BankingApplication;

class ISA : Account
{
    private Customer _accountHolder;
    private decimal APR = 2.75m;
    private int _accountNumber;
    private decimal _balance;
    private decimal _annualInterestAccured;
    private DateTime _createdDate;
    private bool _isActive;

    public ISA(Customer customer, int initialDeposit)
    {
        _accountHolder = customer;
        _accountNumber = GenerateAccountNumber();
        _balance = initialDeposit;
        _createdDate = DateTime.Now;
        _isActive = true;
    }

    private int GenerateAccountNumber()
    {
        return new Random().Next(10000000, 99999999); // Example
    }

    public override void OpenAccount()
    {
        _isActive = true;
    }

    public override void CloseAccount()
    {
        _isActive = false;
    }

    public override void Deposit(decimal amount)
    {
        if (amount <= 0) { throw new ArgumentException("Deposit amount must be greater than 0!"); }
        _balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
        if (_balance - amount < 0) throw new InvalidOperationException("Insufficient funds.");
        _balance -= amount;
    }

    public void CalculateAnnualInterest()
    {
        decimal averageAnnualBalance = _balance; // Simplified for example; should calculate actual average.
        decimal interest = averageAnnualBalance * (APR / 100);
        _balance += interest;
        _annualInterestAccured += interest;
    }
    public string GetAccountDetails()
    {
        return $"Account Number: {_accountNumber}, Balance: {_balance:C}, Annual Interest Rate: {APR}%, Created Date: {_createdDate}, Active: {_isActive}";
    }
}
