namespace WGT_BankingApplication;

class PersonalAccount : Account
{
    private List<DirectDebit> _directDebit;
    private List<StandingOrder> _standingOrder;

    public PersonalAccount()
    {
        _directDebits = new List<DirectDebit>(); //stores direct debits linked to a personal account
        _standingOrder = new List<StandingOrder>(); //stores standing orders linked to a personal account

    }

    public void CreateDirectDebit(string payee, decimal amount, DateTime date, string reference)
    {
        DirectDebit newDirectDebit = new DirectDebit(payee, amount, date, reference);
        _directDebit.Add(newDirectDebit); //add a new direct debit to the list
    }
    public void RemoveDirectDebit(string reference)
    {
        for (int i = 0; i < directDebits.Count; i++) //loop through each direct debit in the list
        {
            if (_directDebits[i].Reference == reference) //check the current direct debit's refernece matches the given reference
            {
                _directDebits.RemoveAt(i); //if it matches, remove the direct debit from the list at the current index
                break; //stop loop as direct debit has been removed
            }
        }
    }
    public void CreateStandingOrder(string payee, decimal amount, DateTime date, string reference)
    {
        StandingOrder newStandingOrder = new StandingOrder(payee, amount, date, reference);
        _standingOrder.Add(newStandingOrder);
    }
    public void RemoveStandingOrder(string reference)
    
        for (int i = 0; i < _standingOrders.Count; i++) //loop through each standing orders in the list
        {
           if (_standingOrders[i].Reference == reference) //check the current stnading order referece matches the given reference
            {
                _standingOrders.RemoveAt(i); //if it matches, remove from the list at the current index
                break; //stop loop as standing order has been removed
            }  
        }
    }

    public override void OpenAccount()
    {
        throw new NotImplementedException();
    }

    public override void CloseAccount()
    {
        throw new NotImplementedException();
    }

    public override void Deposit(decimal amount)
    {
        throw new NotImplementedException();
    }

    public override void Withdraw(decimal amount)
    {
        throw new NotImplementedException();
    }
}
