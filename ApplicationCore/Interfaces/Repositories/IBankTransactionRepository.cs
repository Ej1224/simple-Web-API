using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IBankTransactionRepository
    {
        IQueryable<BankTransaction> bankTransactions { get; }
        string CreateTransaction(BankUserAccount source, BankUserAccount dest, BankTransaction bankTransaction, decimal amount);
    }
}
