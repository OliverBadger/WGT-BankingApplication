using Newtonsoft.Json.Bson;

namespace WGT_BankingApplication;

class BusinessAccount : Account
{
    private string _typeOfBusiness;
    private string _nameOfBusiness;
    private string _businessAddress;
    private bool _isActive;
    private bool _hasRequestedChequeBook;
    private decimal _overdraftAmount = 0m;
    private DateTime _createdDate;
    private DateTime _lastAnnualChargeDate = default;
    private List<Loan> _loans;
    private Customer _accountHolder;
    private List<Cheque> _chequeBook;  

    public string NameOfBusiness { get => _nameOfBusiness; set => _nameOfBusiness = value; }
    public string BusinessAddress { get => _businessAddress; set => _businessAddress = value; }
    public string TypeOfBusiness { get => _typeOfBusiness; set => _typeOfBusiness = value; }
    public bool IsActive { get => _isActive; private set => _isActive = value; }
    public bool HasRequestedChequeBook { get => _hasRequestedChequeBook; private set => _hasRequestedChequeBook = value; }
    // public decimal OverdraftAmount { get => _overdraftAmount; set => _overdraftAmount = value; }
    public DateTime CreatedDate { get => _createdDate; }
    public DateTime LastAnnualChargeDate { get => _lastAnnualChargeDate; private set => _lastAnnualChargeDate = value; }
    public List<Loan> Loans { get => _loans; }
    internal Customer AccountHolder { get => _accountHolder; }
    internal List<Cheque> ChequeBook { get => _chequeBook; }

    public BusinessAccount(Customer Customer, string NameOfBusiness, string TypeOfBusiness, string BusinessAddress, decimal InitialDeposit, decimal OverdraftAmount, bool HasRequestedChequeBook, DateTime CreatedDate = default, DateTime LastAnnualChargeDate = default) : base(Customer.ID, Customer.FirstName, Customer.Surname, Customer.Password, Customer.CustomerNumber)
    {
        _accountHolder = Customer;
        _nameOfBusiness = NameOfBusiness;
        _typeOfBusiness = TypeOfBusiness;
        _businessAddress = BusinessAddress;
        Balance = InitialDeposit;
        _overdraftAmount = OverdraftAmount;
        _createdDate = (CreatedDate == default) ? DateTime.Now: CreatedDate;  // Check for default value (01/01/0001 00:00:00) for future case when reading in accounts, otherwise set todays date
        _lastAnnualChargeDate = AnnualCharge(LastAnnualChargeDate);  // (LastAnnualChargeDate == default) ? AnnualCharge() : LastAnnualChargeDate;
        _isActive = true;
        _loans = [];
        _chequeBook = [];
        _hasRequestedChequeBook = HasRequestedChequeBook;
        Customer.AddAccount(this);
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
        if (_isActive)
        {
            if (amount > 0)
            {
                Balance += amount;
            }
            else
            {
                throw new ArgumentException("Deposit must be a positive, non-zero number");
            }
        }
    }

    public override void Withdraw(decimal amount)
    {
        if (_isActive)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdraw must be a positive, non-zero number");
            }
            else if (Balance + _overdraftAmount < amount)
            {
                throw new InvalidOperationException("Insufficient funds or overdraft limit exceeded.");
            }
            else
            {
                if (Balance == 0)
                {
                    _overdraftAmount -= amount;
                }
                else if (Balance - amount < 0)
                {
                    _overdraftAmount += (Balance - amount);
                }
                else
                {
                    Balance -= amount;
                }
            }  
        }     
    }
    public void RequestNewChequeBook() 
    {
        if (!_hasRequestedChequeBook)
        {
            _hasRequestedChequeBook = true;
        }

        else
        {
            throw new InvalidOperationException("Cheque book already requested.");
        }
    }

    public void WriteACheque(string payeeName, string amountWrittenInWords, decimal amount, string signature, DateTime createdDate = default)
    {
        Cheque cheque = new(payeeName, amountWrittenInWords, amount, signature, createdDate);
        _chequeBook.Add(cheque);
    }
    public DateTime AnnualCharge(DateTime lastDate)  // Updated this to return new LastAnnualChargeDate, pass in a date when reading from file to use in the check otherwise todays date is used.
    {
        //if (!_isActive) Perhaps not required will reinstate later if needed
        //    return DateTime.UnixEpoch;

        DateTime today = DateTime.Now;

        if (_createdDate.Date == today.Date)
        {
            if (Balance >= 120)
            {
                Balance -= 120;
                return today;
            }

            else
            {
                throw new InvalidOperationException("Insufficient balance for annual charge.");
            }
        }

        else if (today.Date == lastDate.AddYears(1).Date)  // Updated to check today is exactly a year later since last charge, before was a year or more
        {
            if (Balance >= 120)
            {
                Balance -= 120;
                _lastAnnualChargeDate = today;
                return today;
            }

            else
            {
                throw new InvalidOperationException("Insufficient balance for annual charge.");
            }
        }
        else
        {
            return lastDate;
        }
    }

    public void RequestNewCard() { }  // Probably going to have to create classes for cards, cheque books and maybe an interface for loan?
    public void InternationalTrade(string certificateOfOrigin,string commercialList, string packingList) { }  // Highly likely I would have to create classes for each of these parameters, they are too complex to just read in
    public void RequestLoan(decimal amount, decimal interestRate, int loanTermInMonths)  // apparently theres no limit to number of loans you can have so no check required
    {
        Loan newLoan = new(amount, interestRate, loanTermInMonths);
        _loans.Add(newLoan);
    }

    public void UpdateAccount()
    {
        if (!_isActive)
            return;

        else
        {
            Console.WriteLine($"Updating Business Account data...");
            // I assume this is where we write to the file to update the customers details, will be implemented in the future
            // Might be an abstract class due to every account needing this functionality
        }
    }
}
