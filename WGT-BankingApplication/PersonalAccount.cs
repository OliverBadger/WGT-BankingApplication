namespace WGT_BankingApplication;

class PersonalAccount : Account
{

    private List<DirectDebit> _directDebit;//Stores direct debit linked to a personal acc
    private List<StandingOrder> _standingOrder;//Stores standing orders linked to personal acc
    private bool _isActive;//Acc is active or not

    //constructor for personal acc
    public PersonalAccount(Customer customer, decimal initialDeposit)
            : base(customer.ID, customer.FirstName, customer.Surname, customer.Password, customer.CustomerNumber)
    {
        _directDebit = new List<DirectDebit>(); // stores direct debits linked to a personal account
        _standingOrder = new List<StandingOrder>(); // stores standing orders linked to a personal account
        Balance = initialDeposit;
        this.InitialDeposit = initialDeposit;
        this.Customer = customer; 
        _isActive = false;//acc starts as inactive
        Customer.AddAccount(this);//adds acc to list of accs
    }

    //property for the initial deposit
        public decimal InitialDeposit { get; }
        //property to get and set to the customer
        public Customer Customer { get; set; }

    //method to create a new direct debit
    public void CreateDirectDebit(string payee, decimal amount, DateTime date, string reference)
        {
            DirectDebit newDirectDebit = new DirectDebit(Customer, InitialDeposit, payee, amount, date, reference);
            _directDebit.Add(newDirectDebit); //add a new direct debit to the list
        }

    //method to remove a direct debit by referance
        public void RemoveDirectDebit(string reference)
        {
            for (int i = 0; i < _directDebit.Count; i++) //loop through each direct debit in the list
            {
                if (_directDebit[i].Reference == reference) //check the current direct debit's reference matches the given reference
                {
                    _directDebit.RemoveAt(i); //if it matches, remove the direct debit from the list at the current index
                    break; //stop loop as direct debit has been removed
                }
            }
        }

        //Method to remove a direct debit by reference
    public void CreateStandingOrder(string payee, decimal amount, DateTime date, string reference)
    {
        StandingOrder newStandingOrder = new StandingOrder(Customer, InitialDeposit, payee, amount, date, reference);
        _standingOrder.Add(newStandingOrder); // add a new standing order to the list
    }

    //method to remove a standing order

    public void RemoveStandingOrder(string reference) { 

            for (int i = 0; i < _standingOrder.Count; i++) //loop through each standing orders in the list
            {
               if (_standingOrder[i].Reference == reference) //check the current standing order reference matches the given reference
                {
                    _standingOrder.RemoveAt(i); //if it matches, remove from the list at the current index
                    break; //stop loop as standing order has been removed
                }  
            }
        }

    //method to open acc
    public override void OpenAccount()
    {
        _isActive = true;
    }

    //method to close acc

    public override void CloseAccount()
    {
        _isActive = false;

    }

    //method to deposit an amount into the acc

    public override void Deposit(decimal amount)
    {
        if (!_isActive)
        {
            Console.WriteLine("Account is closed. Unable to make a deposit"); //can't make deposit if account is closed

            if (amount < 0.01m)
            {
                Console.WriteLine("Deposit must be a positive amount."); //deposit amount should be greater than or equal to 0.01. e.g., can deposit at least 1p
            }
            Balance += amount; //add deposit amount to balance
            Console.WriteLine($"{amount}  deposited successfully. Current Balance: {Balance}");
        }
    }

    //method to withdraw an ammount from the acc

    public override void Withdraw(decimal amount)
    {
        if (!_isActive)
        {
            Console.WriteLine("Account is closed. Cannot withdraw.");
        }

        if (amount < 0.01m) {
            Console.WriteLine("Withdrawal amount must be at least 1p.");
        }

        else
        {
            Balance -= amount; //subtract withdrawal amount from balance 
        }
    }
}