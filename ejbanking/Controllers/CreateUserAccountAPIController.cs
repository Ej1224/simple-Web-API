using ApplicationCore.Interfaces.Services;
using ejbanking.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ejbanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserAccountAPIController : ControllerBase
    {
        private readonly IBankUserAccountServices bankUserAccountServices;

        public CreateUserAccountAPIController(IBankUserAccountServices _bankUserAccountServices)
        {
            bankUserAccountServices = _bankUserAccountServices;
        }

        [HttpPost]
        public BankUserAccountVM CreateAccount(string username, decimal initialBalance = 0.0M)
        {
            string err_thr = "";
            string acct_id = "None";

            Regex regex = new Regex("^[a-zA-Z0-9]*$");

            if(regex.IsMatch(username.Trim()) && initialBalance > 0)
            {
                bool if_exists = bankUserAccountServices.CheckUsername(username);

                if (if_exists)
                {
                    err_thr = "Existing Username";
                }
                else if (if_exists == false)
                {
                    var newAcct_acctId = bankUserAccountServices.CreateAccount(username, initialBalance);
                    acct_id = newAcct_acctId;
                }
            }
            else if (regex.IsMatch(username.Trim()) == false)
            {
                err_thr = "Invalid username";
            }
            else if(initialBalance <= 0)
            {
                err_thr = "Invalid balance";
            }
            

            BankUserAccountVM bankUserAccountVM = new BankUserAccountVM() { ErrorThrown = err_thr, AccountId = acct_id };

            return bankUserAccountVM;
        } 
    }
}
