using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IDispositionService _dispositionService;

        public AccountController(IAccountService accountService, IDispositionService dispositionService)
        {
            _accountService = accountService;
            _dispositionService = dispositionService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post(AccountModel account)
        {
            if(account == null)
            {
                return BadRequest("Felaktiga kontouppgifter");
            }
            var returnedAccount = await _accountService.CreateAccount(account);
            if(returnedAccount != null)
            {
                var returnedDisposition = await _dispositionService.CreateDisposition(
                    account.CustomerId, returnedAccount.AccountId, account.DispositionsType);

                if( returnedDisposition != null )
                {
                    return Ok(returnedAccount);
                }
                else
                {
                    return BadRequest("Gick inte att skapa disposition");
                }
            }
            else
            {
                return BadRequest("Gick inte att skapa konto");
            }

         }
        
    }
}
