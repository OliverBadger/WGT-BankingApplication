namespace WGT_BankingApplication;

class Customer
{
    private int _id;
    private string _customerNumber;
    private string _firstName;
    private string _surname;
    private string _password;
    private ISA? _isa;
    private List<PersonalAccount>? _personalAccounts;
    private List<BusinessAccount>? _businessAccounts;
    private List<Account> _accounts;

    public int ID { get => _id; }
    public string CustomerNumber { get => _customerNumber; set => _customerNumber = value; }
    public string FirstName { get => _firstName; }
    public string Surname { get => _surname; }
    public string Password { get => _password; set => _password = value; } //Changed to public so its serialized - will encrypt
    public ISA? Isa { get => _isa; }
    public List<Account> Accounts { get => _accounts; }
    public List<PersonalAccount>? PersonalAccountList { get => _personalAccounts; }
    public List<BusinessAccount>? BusinessAccountList { get => _businessAccounts; }

    // Could impliment overides for each customer so it takes in the account
    public Customer(int id, string firstname, string surname , string password, string customerNumber)  // Initial testing need to improve 
    {
        _id = id;
        _firstName = firstname;
        _surname = surname;
        _password = password;
        _accounts = new List<Account>();
        _customerNumber = customerNumber;

    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }
}
