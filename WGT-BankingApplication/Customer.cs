﻿namespace WGT_BankingApplication;

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

    public int ID { get { return _id; } }
    public string FirstName { get { return _firstName; } }
    public string Surname { get { return _surname; } }
    public string Password { get { return _password; } set { _password = value; } } //Changed to public so its serialized - will encrypt
    public ISA? Isa { get { return isa; } }
    public List<Account> Account_List { get { return _accounts; } }
    public List<PersonalAccount> PersonalAccount_List { get { return _personalAccounts; } }
    public List<BusinessAccount> BusinessAccount_List { get { return _businessAccounts; } }

    public string CustomerNumber { get => _customerNumber; set => _customerNumber = value; }



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
