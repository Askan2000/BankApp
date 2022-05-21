using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService service)
        {
            _customerService = service;
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int customerId)
        {
            try
            {
                var result = await _customerService.GetCustomer(customerId);
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
                        "Fel vid hämtande av kund från DB");
            }
        }

        //[Authorize]
        [HttpGet("identity/{aspNetId}")]
        public async Task<ActionResult<Customer>> GetCustomerByAspNetId(string aspNetId)
        {

            try
            {
                var result = await _customerService.GetCustomerByAspNetId(aspNetId);
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
                    "Fel vid hämtande av kund från DB");
            }
        }
    }
}
