using System;

namespace WGT_BankingApplication
{
    public class HelpSystem
    {
        //displays the list of available help options
        public void ShowHelpOptions()
        {
            Console.WriteLine("Help Options:");
            Console.WriteLine("1. CreateAccount");
            Console.WriteLine("2. CreateDirectDebit");
            Console.WriteLine("3. CreateStandingOrder");
            Console.WriteLine("4. RemoveDirectDebit");
            Console.WriteLine("5. RemoveStandingOrder");
            Console.WriteLine("6. Deposit");
            Console.WriteLine("7. Withdraw");
            Console.WriteLine("8. CreateBusinessAccount");
            Console.WriteLine("9. ManageISA");
            Console.WriteLine("10. IssueChequeBook");
            Console.WriteLine("11. GenerateCustomer");
            Console.WriteLine("12. ManageLoan");
            Console.WriteLine("13. ManageUserCredentials");
            Console.WriteLine("Type the name of the option for more details.");

        }
        //displays help information based on the context
        public void ShowHelp(string context)
        {
            Console.WriteLine("Help: Select an operation to get help.");
            switch (context)
            {
                case "CreateAccount"://When requiring help with creating an accoount
                    Console.WriteLine("Help: To create a new account, you need to provide valid customer details and an initial deposit.");
                    break;
                case "CreateDirectDebit"://When requiring help with creating a direct debit listing out the details needed
                    Console.WriteLine("Help: To create a direct debit, you need to provide the payee, amount, date, and reference.");
                    break;
                case "CreateStandingOrder"://Providing this message when requiring help with creating a standing order
                    Console.WriteLine("Help: To create a standing order, you need to provide the payee, amount, date, and reference.");
                    break;
                case "RemoveDirectDebit"://Providing help with removing a direct debit
                    Console.WriteLine("Help: To remove a direct debit, you need to provide the reference of the direct debit.");
                    break;
                case "RemoveStandingOrder"://Providng help with making a standing order
                    Console.WriteLine("Help: To remove a standing order, you need to provide the reference of the standing order.");
                    break;
                case "Deposit"://Providing help with deposit money
                    Console.WriteLine("Help: To deposit money, provide the amount the customer wishes to deposit.");
                    break;
                case "Withdraw"://Providing help with withdrawals   
                    Console.WriteLine("Help: To withdraw money, provide the amount the customer wishes to withdraw.");
                    break;
                case "CreateBusinessAccount": // Providing help with creating a business account
                    Console.WriteLine("Help: To create a business account, you need to provide the customer, name of business, type of business, business address, and an initial deposit.");
                    break;
                case "ManageISA": //Providing help with managing an ISA
                    Console.WriteLine("Help: To manage an ISA account, provide the necessary customer details and the action you wish to perform.");
                    break;
                case "IssueChequeBook"://Providing help with issuing a cheque book
                    Console.WriteLine("Help: To issue a cheque book, select the account and confirm the request.");
                    break;
                case "GenerateCustomer"://Provides help for teller to generate a customer
                    Console.WriteLine("Help: To generate a new customer, use the customer generator with the necessary details.");
                    break;
                case "ManageLoan"://Provides help with managing a specific loan already in the system
                    Console.WriteLine("Help: To manage a loan, provide loan details and the action you wish to perform.");
                    break;
                case "ManageUserCredentials"://Provides help with managing user credentials
                    Console.WriteLine("Help: To manage user credentials, provide the necessary user details and the action you wish to perform.");
                    break;
                case "BusinessAccount"://Provides help any business account details
                    Console.WriteLine("Help: Business accounts manage company finances, handle overdrafts, request cheque books, and manage loans.");
                    break;
                default://Any response that is not valid
                    Console.WriteLine("Please select a valid operation.");
                    break;
            }
        }
    }
}
