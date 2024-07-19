namespace WGT_BankingApplication;

// The StandingOrder class inherits from the PersonalAccount class
class StandingOrder : PersonalAccount
{
    private string _payee;//the payee of the standing order
    private decimal _amount;//amount of standing order
    private DateTime _date;//date of standing order
    private string _reference;//reference for standing order


    //constructor for standing order class
    public StandingOrder(Customer Customer, decimal InitialDeposit, string Payee, decimal Amount, DateTime Date, string Reference) : base(Customer, InitialDeposit)
    {
        _payee = Payee;
        _amount = Amount;
        _date = Date;
        _reference = Reference;
    }

    //prop to get and set the reference for the stading order
    public string Reference { get => _reference; set => _reference = value; }  // Added to keep the code working
}
