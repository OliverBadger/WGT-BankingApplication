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
    private int _monthMaturity;
    private int _yearMaturity;
    private List<decimal> _receipts = new List<decimal>();

    public ISA(Customer customer, int initialDeposit)
    {
        _accountHolder = customer;
        _accountNumber = GenerateAccountNumber();
        _balance = initialDeposit;
        _createdDate = DateTime.Now;
        _isActive = true;
        _receipts.Add(_balance);
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
        if (amount <= 0) { throw new ArgumentException("Deposit amount must be greater than 0."); }
        _balance += amount;
        _receipts.Add(_balance);
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
        if (_balance - amount < 0) throw new InvalidOperationException("Insufficient funds.");
        _balance -= amount;
        _receipts.Add(_balance);

    }

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
            _balance += interest;
            _annualInterestAccured += interest;
        }
    }

    public string GetAccountDetails()
    {
        return $"Account Number: {_accountNumber}, Balance: {_balance:C}, Annual Interest Rate: {APR}%, Created Date: {_createdDate}, Active: {_isActive}";
    }

    public bool HasMaturedOneYear()
    {
        return DateTime.Now >= _createdDate.AddYears(1);
    }

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
