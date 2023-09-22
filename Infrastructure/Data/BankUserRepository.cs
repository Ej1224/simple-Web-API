using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;

namespace Infrastructure.Data
{
    public class BankUserRepository : IBankUserRepository
    {
        private readonly CatalogContext _catalogContext;
        public BankUserRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public IQueryable<BankUserAccount> BankUsers => _catalogContext.BankUsers;

        public void CreateUser(BankUserAccount bankUserAccount)
        {
            _catalogContext.Add(bankUserAccount);
            _catalogContext.SaveChanges();
        }

        public void UpdateUser(BankUserAccount bankUserAccount)
        {
            _catalogContext.Update(bankUserAccount);
            _catalogContext.SaveChanges();
        }
    }
}
