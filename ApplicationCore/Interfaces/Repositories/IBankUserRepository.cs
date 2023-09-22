using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories
{
    public interface IBankUserRepository
    {
        IQueryable<BankUserAccount> BankUsers { get; }
        void CreateUser(BankUserAccount bankUserAccount);
        void UpdateUser(BankUserAccount bankUserAccount);
    }
}
