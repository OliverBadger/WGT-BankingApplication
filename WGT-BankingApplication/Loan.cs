namespace WGT_BankingApplication
{
    public class Loan
    {
        private decimal _amount;
        private decimal _interestRate;
        private int _loanTermInMonths;
        private DateTime _startDate;

        public decimal Amount { get => _amount; set => _amount = value; }
        public decimal InterestRate { get => _interestRate; set => _interestRate = value; }
        public int LoanTermInMonths { get => _loanTermInMonths; }
        public DateTime StartDate { get => _startDate; }

        public Loan(decimal Amount, decimal InterestRate, int LoanTermInMonths)  // May have to improve in the future
        {
            _amount = Amount;
            _interestRate = InterestRate;
            _loanTermInMonths = LoanTermInMonths;
            _startDate = DateTime.Now;
        }

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
