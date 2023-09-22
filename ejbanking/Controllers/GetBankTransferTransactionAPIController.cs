using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejbanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetBankTransferTransactionAPIController : ControllerBase
    {
        private readonly IBankTransactionServices _bankTransactionServices;

        public GetBankTransferTransactionAPIController(IBankTransactionServices bankTransactionServices)
        {
            _bankTransactionServices = bankTransactionServices;
        }

        [HttpGet]
        public IEnumerable<BankTransaction> GetBankTransactions()
        {
            return _bankTransactionServices.GetBankTransactions();
        }
    }
}
