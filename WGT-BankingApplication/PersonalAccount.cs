namespace WGT_BankingApplication;

class PersonalAccount : Account
{
    private List<DirectDebit> _directDebit;
    private List<StandingOrder> _standingOrder;

    public PersonalAccount() { }

    public void CreateDirectDebit() { }
    public void RemoveDirectDebit() { }
    public void CreateStandingOrder() { }
    public void RemoveStandingOrder() { }

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
