using Newtonsoft.Json.Bson;

namespace WGT_BankingApplication;

class BusinessAccount : Account
{
    private string? _typeOfBusiness;
    private string? _nameOfBusiness;
    private string? _businessAddress;
    private bool _isActive;
    private bool _hasRequestedChequeBook;
    private decimal _overdraftAmount = 0m;
    private DateTime _createdDate;
    private DateTime _lastAnnualChargeDate = default;
    private List<Loan> _loans; // List of loans associated with the account
    private Customer _accountHolder; // Stores the account holder information
    private List<Cheque> _chequeBook;  // List of cheques issued

    // Properties to access and modify last annual charge date variable
    public DateTime LastAnnualChargeDate { get => _lastAnnualChargeDate; private set => _lastAnnualChargeDate = value; }

    // Constructor to initialise a business account
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
        Customer.BusinessAccount = this;
        //Customer.AddAccount(this); // Add this account to the customer's list of accounts
    }

    // Activates account
    public override void OpenAccount()
    {
        _isActive = true;
    }

    // Deactivates account
    public override void CloseAccount()
    {
        _isActive = false;
    }

    // Deposit money into the account
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

    // Withdraw money from the account
    public override void Withdraw(decimal amount)
    {
        // If the account is not active then the code is unable to run
        if (_isActive)
        {
            // If the amount is negative or 0 then a new argument is thrown
            if (amount <= 0)
            {
                throw new ArgumentException("Withdraw must be a positive, non-zero number");
            }
            // Checks for available funds and overdraft amount then throws an argument if exceeded
            else if (Balance + _overdraftAmount < amount)
            {
                throw new InvalidOperationException("Insufficient funds or overdraft limit exceeded.");
            }
            // If the checks have passed the validation for your bank balance and overdraft is calculated based on the withdrawal amount
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

    // Request a new cheque book
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

    // Calculate the annual charge and update the last annual charge date
    public DateTime AnnualCharge(DateTime lastDate)  // Method to calculate and deduct the annual charge, and update the last annual charge date.
    {
        // DateTime object to store the current date and time
        DateTime today = DateTime.Now;

        // Check if the account creation date is today
        if (_createdDate.Date == today.Date)
        {
            // If balance is sufficient, deduct the annual charge and return today's date
            if (Balance >= 120)
            {
                Balance -= 120;
                return today;
            }
            else
            {
                // If balance is insufficient, throw an exception
                throw new InvalidOperationException("Insufficient balance for annual charge.");
            }
        }
        // Check if today is exactly one year after the last annual charge date
        else if (today.Date == lastDate.AddYears(1).Date)
        {
            // If balance is sufficient, deduct the annual charge, update the last annual charge date, and return today's date
            if (Balance >= 120)
            {
                Balance -= 120;
                _lastAnnualChargeDate = today;
                return today;
            }
            else
            {
                // If balance is insufficient, throw an exception
                throw new InvalidOperationException("Insufficient balance for annual charge.");
            }
        }
        else
        {
            // If the account creation date is not today and it is not exactly one year since the last charge, return the last charge date
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
