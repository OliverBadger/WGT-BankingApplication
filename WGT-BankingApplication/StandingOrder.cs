namespace WGT_BankingApplication;

class StandingOrder : PersonalAccount
{
    private string _payee;
    private decimal _amount;
    private DateTime _date;
    private string _reference;

    public StandingOrder(Customer Customer, int InitialDeposit, string Payee, decimal Amount, DateTime Date, string Reference) : base(Customer, InitialDeposit)
    {
        _payee = Payee;
        _amount = Amount;
        _date = Date;
        _reference = Reference;
    }

    public string Reference { get => _reference; set => _reference = value; }  // Added to keep the code working
}
