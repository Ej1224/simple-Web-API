using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Interfaces.Services;

namespace ApplicationCore.Services
{
    public class BankTransactionServices : IBankTransactionServices
    {
        private readonly IBankTransactionRepository _bankTransactionRepository;

        public BankTransactionServices(IBankTransactionRepository bankTransactionRepository)
        {
            _bankTransactionRepository = bankTransactionRepository;
        }


        public string CreateTransaction(string sourceAcctId, string destinationAcctId, decimal transferAmount)
        {
            int lastId = 1;
            string transactionID = "";

            if (_bankTransactionRepository.bankTransactions.Count() > 0)
            {
                lastId = _bankTransactionRepository.bankTransactions.OrderByDescending(bt => bt.Id).First().Id;
                lastId++;

                transactionID = "T" + DateTime.Now.ToString("yyyyMMdd") + lastId;
            }
            else if (_bankTransactionRepository.bankTransactions.Count() == 0)
            {
                transactionID = "T" + DateTime.Now.ToString("yyyyMMdd") + lastId;
            }

            BankTransaction transaction = new BankTransaction(transactionID, "Transfer", sourceAcctId, destinationAcctId, transferAmount);

            _bankTransactionRepository.CreateTransaction(transaction);

            return transactionID;
        }

        public IEnumerable<BankTransaction> GetBankTransactions()
        {
            return _bankTransactionRepository.bankTransactions;
        }
    }
}
