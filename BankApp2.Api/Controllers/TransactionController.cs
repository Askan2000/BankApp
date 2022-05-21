using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    //[Authorize(Roles = "Customer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService service)
        {
            _transactionService = service;
        }

        [HttpPost]
        public async Task<ActionResult<NewTransaction>> CreateTransaction(NewTransaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    var createdTransaction = await _transactionService.CreateTransaction(transaction);

                    return Ok(createdTransaction);
                    //return CreatedAtAction(nameof(GetCustomer),
                    //    new { id = createdCustomer.CustomerId }, createdCustomer);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fel i registrering av transaktion");
            }
        }
        [HttpGet("{accountId}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int accountId)
        {
            try
            {
                var result = await _transactionService.GetTransaction(accountId);
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fel vid hämtande av transaktion från DB");
            }
        }
    }
}
