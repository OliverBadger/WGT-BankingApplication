namespace WGT_BankingApplication
{
    public class Loan
    {
        private decimal _amount;//principal amount of the loan
        private decimal _interestRate;//annual interest rate of the loan
        private int _loanTermInMonths;//loan term in months
        private DateTime _startDate;//date when the loan starts

        //gets or sets the loan amount
        public decimal Amount { get => _amount; set => _amount = value; }
        //gets or sets the loan interest rate
        public decimal InterestRate { get => _interestRate; set => _interestRate = value; }
        //gets the loan term in months
        public int LoanTermInMonths { get => _loanTermInMonths; }
        //gets the loan start date
        public DateTime StartDate { get => _startDate; }


        //sets the loan with specified amount, interest rate, and loan term
        public Loan(decimal Amount, decimal InterestRate, int LoanTermInMonths)  // May have to improve in the future
        {
            _amount = Amount;
            _interestRate = InterestRate;
            _loanTermInMonths = LoanTermInMonths;
            _startDate = DateTime.Now;
        }

        //calculates the monthly payment based on the loan amount, interest tate and loan term using the formula
        public decimal CalculateMonthlyPayment()
        {
            decimal monthlyInterestRate = _interestRate / 12 / 100;
            decimal numerator = monthlyInterestRate * (decimal)Math.Pow(1 + (double)monthlyInterestRate, _loanTermInMonths);
            decimal denominator = (decimal)Math.Pow(1 + (double)monthlyInterestRate, _loanTermInMonths) - 1;
            
            decimal monthlyPayment = _amount * numerator / denominator;

            return monthlyPayment;

        }
    }
}
