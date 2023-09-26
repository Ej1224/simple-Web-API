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
            string retVal = "";

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
                                retVal = "Insufficient funds";
                            }
                            else if (sourceBalance_afterDeduct == 0)
                            {
                                retVal = "Account balance cannot be lower than 0";
                            }
                            else if (sourceBalance_afterDeduct > 0)
                            {
                                Thread.Sleep(5000);
                                retVal = _bankTransactionServices.CreateTransaction(sourceUser, destUser, amount);
                            }
                        }
                        else if (sourceUser == destUser)
                        {
                            retVal = "Source account and destination account cannot be the same";
                        }
                    }
                    else if (sourceUser == null)
                    {
                        retVal = "Source account number not exists";
                    }
                    else if (destUser == null)
                    {
                        retVal = "Destination account number not exists";
                    }
                    else if (amount < 0)
                    {
                        retVal = "Invalid transfer amount";
                    }
                }
                else if(sourceAcctId.Trim() == "")
                {
                    retVal = "Invalid source account number";
                }
                else if(destAcctId.Trim() == "")
                {
                    retVal = "Invalid destination account number";
                }
                else if(amount <= 0)
                {
                    retVal = "Invalid transfer amount";
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                retVal = "Concurrency Exception";
            }

            BankTransactionVM result = new BankTransactionVM() { ReturnVal = retVal };
            return result;
        }
    }
}
