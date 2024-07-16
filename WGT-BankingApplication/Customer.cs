namespace WGT_BankingApplication;

class Customer
{
    private string _name;
    private string _password;
    public List<Account> _accounts { get; set; }

    public Customer()
    {
        _accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
}
