using BankApp2.Core.Interfaces;
using BankApp2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {

            try
            {
                return Ok(await _service.GetAllCustomers());

            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int customerId)
        {

            try
            {
                var result = await _service.GetCustomer(customerId);
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
        [HttpGet("identity/{aspNetId}")]
        public async Task<ActionResult<Customer>> GetCustomerByAspNetId(string aspNetId)
        {

            try
            {
                var result = await _service.GetCustomerByAspNetId(aspNetId);
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
        //[HttpPost]
        //public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        //{
        //    try
        //    {
        //        if (customer != null)
        //        {
        //            var createdCustomer = await _service.AddCustomer(customer);

        //            return Ok(createdCustomer);
        //            //return CreatedAtAction(nameof(GetCustomer),
        //            //    new { id = createdCustomer.CustomerId }, createdCustomer);
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        return StatusCode(StatusCodes.Status500InternalServerError, 
        //            "Fel i registrering av ny kund");
        //    }

        //}
    }
}
