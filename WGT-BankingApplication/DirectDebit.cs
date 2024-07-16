namespace WGT_BankingApplication;

class DirectDebit : PersonalAccount
{
	private string _payee;
	private decimal _amount;
	private DateTime _date;
	private string _reference;

	public DirectDebit(string Payee, decimal Amount, DateTime Date, string Reference)
	{
		_payee = Payee;
		_amount = Amount;
		_date = Date;
		_reference = Reference;
	}
}
