namespace WGT_BankingApplication;

class ISA : Account
{
    private Customer _accountHolder;
    private decimal APR = 2.75m;
    private int _accountNumber;
    private decimal _annualInterestAccured;
    private DateTime _createdDate;
    private bool _isActive;
    private int _monthMaturity;
    private int _yearMaturity;
    private List<decimal> _receipts = new List<decimal>();

    public ISA(Customer Customer, int InitialDeposit) : base(Customer.ID, Customer.FirstName, Customer.Surname, Customer.Password)
    {
        _accountHolder = Customer;
        _accountNumber = GenerateAccountNumber();
        Balance = InitialDeposit;
        _createdDate = DateTime.Now;
        _isActive = true;
        _receipts.Add(Balance);
        Customer.AddAccount(this);
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
        Balance += amount;
        _receipts.Add(Balance);
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be positive.");
        if (Balance - amount < 0) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        _receipts.Add(Balance);

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
            Balance += interest;
            _annualInterestAccured += interest;
        }
    }

    public string GetAccountDetails()
    {
        return $"Account Number: {_accountNumber}, Balance: {Balance:C}, Annual Interest Rate: {APR}%, Created Date: {_createdDate}, Active: {_isActive}";
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
