namespace WGT_BankingApplication;

class Customer
{
    private int _id;
    private string _name;
    private string _password;
    private List<Account> _accounts;

    public int ID { get { return _id; } }
    public string Name { get { return _name; } }
    private string Password { get { return _password; } set { _password = value; } }
    public List<Account> Account_List { get { return _accounts; } }

    public Customer(int id, string name, string password)  // Initial testing need to improve 
    {
        _id = id;
        _name = name;
        _password = password;
        _accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
}
