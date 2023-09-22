using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ejbanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUserAccountAPIController : ControllerBase
    {
        private readonly IBankUserAccountServices _bankUserAccountServices;

        public GetUserAccountAPIController(IBankUserAccountServices bankUserAccountServices)
        {
            _bankUserAccountServices = bankUserAccountServices;
        }

        [HttpGet]
        public IEnumerable<BankUserAccount> GetUserAccounts()
        {
            return _bankUserAccountServices.GetBankUserAccounts();
        }
    }
}
