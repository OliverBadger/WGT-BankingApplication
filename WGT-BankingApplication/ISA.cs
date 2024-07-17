namespace WGT_BankingApplication;

class ISA : Account
{
    private decimal APR = 2.75m;

    public ISA() { }

    public void AccumulateInterest() { }

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
