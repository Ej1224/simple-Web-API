using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IBankTransactionServices
    {
        string CreateTransaction(BankUserAccount source, BankUserAccount dest, decimal transferAmount);
        IEnumerable<BankTransaction> GetBankTransactions();
    }
}
