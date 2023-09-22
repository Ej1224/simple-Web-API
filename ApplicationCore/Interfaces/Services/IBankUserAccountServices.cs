using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Services
{
    public interface IBankUserAccountServices
    {
        string CreateAccount(string username, decimal initialBal);
        bool CheckUsername(string username);
        BankUserAccount? GetBankUserAccount(string accountId);
        void UpdateBankUserAccount(BankUserAccount bankUserAccount);
        IEnumerable<BankUserAccount> GetBankUserAccounts();
    }
}