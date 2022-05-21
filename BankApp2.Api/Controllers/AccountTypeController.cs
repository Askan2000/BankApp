using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Admin")]
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

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AccountType>> CreateTransaction(AccountTypeDto accountType)
        {
            try
            {
                if (accountType != null)
                {
                    var createdAccountType = await _accountTypeService.CreateAccountType(accountType);

                    return Ok(createdAccountType);
                }
                else
                {
                    return BadRequest("Fel kontotypsuppgifter");
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fel i registrering av kontotyp");
            }

        }


    }
}
