namespace WGT_BankingApplication;

class PersonalAccount : Account
{
    private List<DirectDebit> _directDebit;
    private List<StandingOrder> _standingOrder;
    private bool _isActive;

    public PersonalAccount()
    {
        _directDebit = new List<DirectDebit>(); //stores direct debits linked to a personal account
        _standingOrder = new List<StandingOrder>(); //stores standing orders linked to a personal account
        private bool _isActive = false;
    }

public void CreateDirectDebit(string payee, decimal amount, DateTime date, string reference)
    {
        DirectDebit newDirectDebit = new DirectDebit(payee, amount, date, reference);
        _directDebit.Add(newDirectDebit); //add a new direct debit to the list
    }
    public void RemoveDirectDebit(string reference)
    {
        for (int i = 0; i < _directDebit.Count; i++) //loop through each direct debit in the list
        {
            if (_directDebit[i].Reference == reference) //check the current direct debit's refernece matches the given reference
            {
                _directDebit.RemoveAt(i); //if it matches, remove the direct debit from the list at the current index
                break; //stop loop as direct debit has been removed
            }
        }
    }
    public void CreateStandingOrder(string payee, decimal amount, DateTime date, string reference)
    {
        StandingOrder newStandingOrder = new StandingOrder(payee, amount, date, reference);
        _standingOrder.Add(newStandingOrder);
    }
    public void RemoveStandingOrder(string reference) { 
    
        for (int i = 0; i < _standingOrder.Count; i++) //loop through each standing orders in the list
        {
           if (_standingOrder[i].Reference == reference) //check the current stnading order referece matches the given reference
            {
                _standingOrder.RemoveAt(i); //if it matches, remove from the list at the current index
                break; //stop loop as standing order has been removed
            }  
        }
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
        if (!_isActive)
    {
        throw new InvalidOperationException("Accont is closed. Unable to make a deposit"); //can't make deposit if account is closed
  
        if (amount < 0.01m)
    {
        throw new ArgumentException("Deposit must be a positive amount."); //depsiot amount should be greater than or equal to 0.01. e.g., can deposit at least 1p
    }
    Balance += amount; //add deposit amount to balance
    Console.WriteLine($"{amount}  deposited successfully. Current Balance: {Balance}");
    }

    public override void Withdraw(decimal amount)
    {
        if (!_isActive)
    {
        Console.WriteLine("Account is closed. Cannot withdraw.");
    }
        elseif (amount <0.01m)
    {
        Console.WriteLine("Withdrawal amount must be at least 1p.");
    }
    else
    {
        Balance -= amount; //subtract withdrawal amount from balance
        Console.WriteLine($"{amount} withdrawn successfully. Current balance: {Balance}");
    }
}
