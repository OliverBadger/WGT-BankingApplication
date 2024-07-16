namespace WGT_BankingApplication;

abstract class Account
{
    public Customer AccountHolder { get; set; }
    public abstract void Deposit(double amount);
    public abstract void Withdraw(double amount);
}
