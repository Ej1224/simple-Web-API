using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IBankTransactionRepository
    {
        IQueryable<BankTransaction> bankTransactions { get; }
        void CreateTransaction(BankTransaction transaction);
    }
}
