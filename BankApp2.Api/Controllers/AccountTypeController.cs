using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTypeController : ControllerBase
    {
        private readonly IAccountTypeService _accountTypeService;

        public AccountTypeController(IAccountTypeService accountTypeService)
        {
            _accountTypeService = accountTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountType>>> GetAccountTypes()
        {
            try
            {
                return Ok(await _accountTypeService.GetAccountTypes());
            }
            catch (Exception)
            {

                return NotFound("Inga kontotyper hittades");
            }
        }
            

    }
}
