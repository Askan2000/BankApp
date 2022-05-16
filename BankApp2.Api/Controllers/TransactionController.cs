using BankApp2.Core.Interfaces;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<NewTransaction>> CreateTransaction(NewTransaction transaction)
        {
            try
            {
                if (transaction != null)
                {
                    var createdTransaction = await _service.CreateTransaction(transaction);

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
    }
}
