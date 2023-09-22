using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IBankTransactionServices
    {
        string CreateTransaction(string sourceAcctId, string destinationAcctId, decimal transferAmount);
        IEnumerable<BankTransaction> GetBankTransactions();
    }
}
