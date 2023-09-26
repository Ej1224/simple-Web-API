using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Data
{
    public class BankTransactionRepository : IBankTransactionRepository
    {
        private readonly CatalogContext _catalogContext;
        public BankTransactionRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public IQueryable<BankTransaction> bankTransactions => _catalogContext.BankTransactions;

        public string CreateTransaction(BankUserAccount source, BankUserAccount dest, BankTransaction bankTransaction, decimal amount)
        {
            using var transaction = _catalogContext.Database.BeginTransaction();

            try
            {
                source.AccountBalance -= amount;
                dest.AccountBalance += amount;

                _catalogContext.Update(source);
                _catalogContext.Update(dest);

                _catalogContext.Add(bankTransaction);

                _catalogContext.SaveChanges();
                transaction.Commit();

                return bankTransaction.TransactionId;
            }
            catch (Exception)
            {
                return "Concurrency Exception";
            }
        }
    }
}
