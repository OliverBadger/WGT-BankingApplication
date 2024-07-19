namespace WGT_BankingApplication
{
    class Cheque
    {
        private DateTime _createdDate;
        private string _payeeName;
        private string _amountWrittenInWords;
        private string _signature;
        private decimal _amount;

        public DateTime CreatedDate { get => _createdDate;}
        public string PayeeName { get => _payeeName;}
        public string AmountWrittenInWords { get => _amountWrittenInWords; }
        public decimal Amount { get => _amount; }
        public string Signature { get => _signature; }

        public Cheque(string PayeeName, string AmountWrittenInWords, decimal Amount, string Signature, DateTime CreatedDate = default) 
        {
            _payeeName = PayeeName;
            _amountWrittenInWords = AmountWrittenInWords;
            _signature = Signature;
            _amount = Amount;
            _createdDate = CreatedDate;

        }

        public void SaveChequeToFile()  // Need to update, might have to split into separate lines to keep formatting using padright/left
        {
            string cheque = $"""
                #####                   DATE: {_createdDate}
                #####
                #####

                PAY:__{_payeeName}___________________________
                ____{_amountWrittenInWords}______{_amount:C}_
                ______________________________{_signature}___
                """;

        }
    }
}
