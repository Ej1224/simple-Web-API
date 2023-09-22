using ApplicationCore.Interfaces.Services;
using ejbanking.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ejbanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferMoneyAPIController : ControllerBase
    {
        private readonly IBankTransactionServices _bankTransactionServices;
        private readonly IBankUserAccountServices _bankUserAccountServices;

        public TransferMoneyAPIController(IBankTransactionServices bankTransactionServices,
                                          IBankUserAccountServices bankUserAccountServices)
        {
            _bankTransactionServices = bankTransactionServices;
            _bankUserAccountServices = bankUserAccountServices;

        }

        [HttpPost]
        public BankTransactionVM TransferMoney(string sourceAcctId, string destAcctId, decimal amount = 0.0M)
        {
            string errThr = "", transactionID = "";

            try
            {
                if(sourceAcctId.Trim() != "" && destAcctId.Trim() != "" && amount > 0)
                {
                    var sourceUser = _bankUserAccountServices.GetBankUserAccount(sourceAcctId);
                    var destUser = _bankUserAccountServices.GetBankUserAccount(destAcctId);

                    if (sourceUser != null && destUser != null && amount > 0)
                    {
                        if (sourceUser != destUser)
                        {
                            decimal sourceBalance_afterDeduct = (decimal)(sourceUser.AccountBalance - amount);

                            if (sourceBalance_afterDeduct < 0)
                            {
                                errThr = "Insufficient funds";
                            }
                            else if (sourceBalance_afterDeduct == 0)
                            {
                                errThr = "Account balance cannot be lower than 0";
                            }
                            else if (sourceBalance_afterDeduct > 0)
                            {
                                sourceUser.AccountBalance = sourceBalance_afterDeduct;
                                _bankUserAccountServices.UpdateBankUserAccount(sourceUser);

                                destUser.AccountBalance += amount;
                                _bankUserAccountServices.UpdateBankUserAccount(destUser);

                                transactionID = _bankTransactionServices.CreateTransaction(sourceAcctId, destAcctId, amount);
                            }
                        }
                        else if (sourceUser == destUser)
                        {
                            errThr = "Source account and destination account cannot be the same";
                        }
                    }
                    else if (sourceUser == null)
                    {
                        errThr = "Source account number not exists";
                    }
                    else if (destUser == null)
                    {
                        errThr = "Destination account number not exists";
                    }
                    else if (amount < 0)
                    {
                        errThr = "Invalid transfer amount";
                    }
                }
                else if(sourceAcctId.Trim() == "")
                {
                    errThr = "Invalid source account number";
                }
                else if(destAcctId.Trim() == "")
                {
                    errThr = "Invalid destination account number";
                }
                else if(amount <= 0)
                {
                    errThr = "Invalid transfer amount";
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                errThr = "Concurrency Exception";
            }

            BankTransactionVM result = new BankTransactionVM() { ErrorThrown = errThr, TransactionID = transactionID };
            return result;
        }
    }
}
