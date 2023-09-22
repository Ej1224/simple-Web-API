using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;

namespace ApplicationCore.Services
{
    public class BankUserAccountServices : IBankUserAccountServices
    {
        private readonly IBankUserRepository _repository;
        public BankUserAccountServices(IBankUserRepository bankUserRepository)
        {
            _repository = bankUserRepository;
        }

        public bool CheckUsername(string username)
        {
            bool exists = false;

            var existingUser = _repository.BankUsers.Where(us => us.Username == username).FirstOrDefault();
            if (existingUser != null)
            {
                exists = true;
            }

            return exists;
        }

        public string CreateAccount(string username, decimal initialBal)
        {
            int lastId = 1;
            string acctId = "";

            if (_repository.BankUsers.Count() > 0)
            {
                lastId = _repository.BankUsers.OrderByDescending(bu => bu.Id).First().Id;
                lastId++;

                acctId = DateTime.Now.ToString("yyyyMMddhhmmss") + lastId;
            }
            else if (_repository.BankUsers.Count() == 0)
            {
                acctId = DateTime.Now.ToString("yyyyMMddhhmmss") + lastId;
            }

            var newBankAccount = new BankUserAccount(username, initialBal);
            newBankAccount.AccountId = acctId;

            _repository.CreateUser(newBankAccount);
            return acctId;
        }

        public BankUserAccount? GetBankUserAccount(string accountId)
        {
            var user = _repository.BankUsers.Where(user => user.AccountId == accountId).FirstOrDefault();
            return user;
        }

        public IEnumerable<BankUserAccount> GetBankUserAccounts()
        {
            return _repository.BankUsers;
        }

        public void UpdateBankUserAccount(BankUserAccount bankUserAccount)
        {
            _repository.UpdateUser(bankUserAccount);
        }
    }
}
