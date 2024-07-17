namespace WGT_BankingApplication;

class BusinessAccount : Account
{
    private bool _isActive;
    private bool _hasRequestedChequeBook; 
    private decimal _balance;  // Might need to remove in the future to use from Account class
    private decimal _overdraftAmount;
    private DateTime _createdDate;
    private DateTime _lastAnnualChargeDate;
    private List<Loan> _loans;
    private Customer _accountHolder;
    private ChequeBook? _chequeBook;  // Lazy implementation, potentially going to split up into cheque class and make a chequebook a list of cheques?

    public bool IsActive { get => _isActive; private set => _isActive = value; }
    public bool HasRequestedChequeBook { get => _hasRequestedChequeBook; private set => _hasRequestedChequeBook = value; }
    public decimal Balance { get => _balance; private set => _balance = value; }  // Need to add override to allow BusinessAccount class to set values
    public decimal OverdraftAmount { get => _overdraftAmount; set => _overdraftAmount = value; }
    public DateTime CreatedDate { get => _createdDate; }
    public DateTime LastAnnualChargeDate { get => _lastAnnualChargeDate; private set => _lastAnnualChargeDate = value; }
    public List<Loan> Loans { get => _loans; }
    internal Customer AccountHolder { get => _accountHolder; }
    internal ChequeBook? ChequeBook { get => _chequeBook; private set => _chequeBook = value;  }

    public BusinessAccount(Customer Customer, decimal InitialDeposit) : base()
    {
        _accountHolder = Customer;
        _balance = InitialDeposit;
        _createdDate = DateTime.Now;
        _lastAnnualChargeDate = _createdDate;
        _isActive = true;
        _overdraftAmount = 0;
        _loans = new List<Loan>();
        _hasRequestedChequeBook = false;
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
                _balance += amount;
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
            else if (_balance + _overdraftAmount < amount)
            {
                throw new InvalidOperationException("Insufficient funds or overdraft limit exceeded.");
            }
            else
            {
                _balance -= amount;
            }
        }
            
    }
    public void RequestNewChequeBook() 
    {
        if (!_hasRequestedChequeBook)
        {
            _chequeBook = new ChequeBook();
            _hasRequestedChequeBook = true;
        }

        else
        {
            throw new InvalidOperationException("Cheque book already requested.");
        }
    }
    public void AnnualCharge()
    {
        if (!_isActive)
            return;

        DateTime today = DateTime.Now;
        if (today >= _lastAnnualChargeDate.AddYears(1))
        {
            if (_balance >= 120)
            {
                _balance -= 120;
                _lastAnnualChargeDate = today;
            }

            else
            {
                throw new InvalidOperationException("Insufficient balance for annual charge.");
            }
        }
    }

    public void RequestNewCard() { }  // Probably going to have to create classes for cards, cheque books and maybe an interface for loan?
    public void RequestLoan(decimal amount, decimal interestRate, int loanTermInMonths)  // apparently theres no limit to number of loans you can have so no check required
    {
        Loan newLoan = new Loan(amount, interestRate, loanTermInMonths);
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
