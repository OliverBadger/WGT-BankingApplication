using System;

namespace WGT_BankingApplication
{
    public class HelpSystem
    {
        public void ShowHelp(string context)
        {
            Console.WriteLine("Help: Select an operation to get help.");
            switch (context)
            {
                case "CreateAccount":
                    Console.WriteLine("Help: To create a new account, you need to provide valid customer details and an initial deposit.");
                    break;
                case "CreateDirectDebit":
                    Console.WriteLine("Help: To create a direct debit, you need to provide the payee, amount, date, and reference.");
                    break;
                case "CreateStandingOrder":
                    Console.WriteLine("Help: To create a standing order, you need to provide the payee, amount, date, and reference.");
                    break;
                case "RemoveDirectDebit":
                    Console.WriteLine("Help: To remove a direct debit, you need to provide the reference of the direct debit.");
                    break;
                case "RemoveStandingOrder":
                    Console.WriteLine("Help: To remove a standing order, you need to provide the reference of the standing order.");
                    break;
                case "Deposit":
                    Console.WriteLine("Help: To deposit money, provide the amount the customer wishes to deposit.");
                    break;
                case "Withdraw":
                    Console.WriteLine("Help: To withdraw money, provide the amount the customer wishes to withdraw.");
                    break;
                case "CreateBusinessAccount":
                    Console.WriteLine("Help: To create a business account, you need to provide the customer, name of business, type of business, business address, and an initial deposit.");
                    break;
                case "ManageISA":
                    Console.WriteLine("Help: To manage an ISA account, provide the necessary customer details and the action you wish to perform.");
                    break;
                case "IssueChequeBook":
                    Console.WriteLine("Help: To issue a cheque book, select the account and confirm the request.");
                    break;
                case "GenerateCustomer":
                    Console.WriteLine("Help: To generate a new customer, use the customer generator with the necessary details.");
                    break;
                case "ManageLoan":
                    Console.WriteLine("Help: To manage a loan, provide loan details and the action you wish to perform.");
                    break;
                case "ManageUserCredentials":
                    Console.WriteLine("Help: To manage user credentials, provide the necessary user details and the action you wish to perform.");
                    break;
                case "BusinessAccount":
                    Console.WriteLine("Help: Business accounts manage company finances, handle overdrafts, request cheque books, and manage loans.");
                    break;
                default:
                    Console.WriteLine("Please select a valid operation.");
                    break;
            }
        }
    }
}
