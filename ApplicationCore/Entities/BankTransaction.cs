namespace ApplicationCore.Entities
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public string? TransactionId { get; set; }
        public string? TransactionName { get; set; }
        public string? SourceAccountId { get; set; }
        public string? DestinationAccountId { get; set;}
        public decimal? TransferAmount { get; set; }

        public BankTransaction(string transactionId,
                               string transactionName,
                               string sourceAccountId,
                               string destinationAccountId,
                               decimal transferAmount)
        {
            TransactionId = transactionId;
            TransactionName = transactionName;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            TransferAmount = transferAmount;
        }

        private BankTransaction() { }
    }
}
