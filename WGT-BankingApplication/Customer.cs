using Newtonsoft.Json;

namespace WGT_BankingApplication;

class Customer
{
    // private detail to store the customer details
    private int _id;
    private string _customerNumber;
    private string _firstName;
    private string _surname;
    private string _password;
    private ISA? _isa;
    private BusinessAccount? _businessAccount;
    private List<PersonalAccount>? _personalAccounts;
    
    // Change remove list? 
    //private List<Account> _accounts;
    
    // Getters and setters for access to the variables
    public int ID { get => _id; }
    public string CustomerNumber { get => _customerNumber; set => _customerNumber = value; }
    [JsonProperty("FirstName")]
    public string FirstName { get => _firstName; }
    [JsonProperty("SecondName")]
    public string Surname { get => _surname; }
    public string Password { get => _password; set => _password = value; } //Changed to public so its serialized - will encrypt
    public ISA? Isa { get => _isa; set => _isa = value; }
    //public List<Account> Accounts { get => _accounts; }
    public List<PersonalAccount>? PersonalAccountList { get => _personalAccounts; }
    public BusinessAccount? BusinessAccount { get => _businessAccount; set => _businessAccount = value; }

    // Could impliment overides for each customer so it takes in the account
    // Constructor to initialize a new Customer object
    public Customer(int id, string firstname, string surname , string password, string customerNumber)  // Initial testing need to improve 
    {
        _id = id;
        _firstName = firstname;
        _surname = surname;
        _password = password;
        //_accounts = new List<Account>(); // Initializes the account list
        _customerNumber = customerNumber;

    }

    // Method to add an account to the customers list of account
    public virtual void AddAccount(Account account)
    {
        //_accounts.Add(account); 
    }
}
