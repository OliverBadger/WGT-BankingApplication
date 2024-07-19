namespace WGT_BankingApplication;

class DirectDebit : PersonalAccount
{
    private string _payee;         // The entity receiving the payment
    private decimal _amount;       // The amount to be debited
    private DateTime _date;        // The date of the transaction
    private string _reference;     // Reference for the transaction

    // Constructor to initialize DirectDebit with given details
    public DirectDebit(Customer customer, decimal initialDeposit, string payee, decimal amount, DateTime date, string reference)
        : base(customer, initialDeposit)
    {
        _payee = payee;              // Set the payee
        _amount = amount;           // Set the amount
        _date = date;               // Set the transaction date
        _reference = reference;     // Set the transaction reference
    }

    // Gets and Sets transaction reference
    public string Reference { get => _reference; set => _reference = value; }  // Added to keep the code working
}
