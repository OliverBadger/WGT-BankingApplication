namespace WGT_BankingApplication
{
    public class Loan
    {
        private decimal _amount;
        private decimal _interestRate;
        private DateTime _startDate;
        private int _loanTermInMonths;

        public decimal Amount { get { return _amount; } set { _amount = value; } }
        public decimal InterestRate { get { return _interestRate; } set { _interestRate = value; } }
        public DateTime StartDate { get { return _startDate; }}
        public int LoanTermInMonths { get => _loanTermInMonths; }

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
