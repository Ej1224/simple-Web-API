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

        public void CreateTransaction(BankTransaction transaction)
        {
            _catalogContext.Add(transaction);
            _catalogContext.SaveChanges();
        }
    }
}
