namespace WGT_BankingApplication;

class BusinessAccount : Account
{
    private decimal _overdraftAmount;
    public BusinessAccount() { }

    public void RequestNewCard() { }  // Probably going to have to create classes for cards, cheque books and maybe an interface for loan?
    public void RequestNewChequeBook() { }
    public void RequestLoan() { }

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
