using BankApp2.Core.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankApp2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAspNetUserService _aspNetUserService;
        private readonly ICustomerService _customerService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IAspNetUserService service, ICustomerService customerService, IJwtTokenService jwtTokenService)
        {
            _aspNetUserService = service;
            _customerService = customerService;
            _jwtTokenService = jwtTokenService;
        }

        //[Authorize(Roles ="Admin")]
        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register (UserRegisterDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Felktiga uppgifter");
                }
                else
                {
                    var result = await _aspNetUserService.CreateIdentityAccount(user);

                    if (result != null)
                    {
                        //Om det gick bra att skapa AspNetUsern vill jag hämta upp denna och lägga in en Customer med AspNetUserId:et på
                        var aspNetUserId = await _aspNetUserService.GetIdentityUserId(result);

                        if (aspNetUserId != null)
                        {
                            var resultCustomer = await _customerService.AddCustomer(user, aspNetUserId);

                            if (resultCustomer != null)
                            {
                                return Ok(resultCustomer);
                            }
                            else
                            {
                                return BadRequest("Gick inte att skapa Customer");
                            }
                        }
                        else
                        {
                            return BadRequest("Gick inte att hämta AspNetUserId");
                        }
                    }
                    else
                    {
                        return BadRequest("Gick inte att skapa AspNetUser");
                    }
                }   
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }       
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Felaktiga inloggningsuppgifter");
                }
                else
                {
                    //prova att logga in användaren i Identity
                    var result = await _aspNetUserService.LoginIdentityAccount(user);

                    if (result != null)
                    {
                        string token = await _jwtTokenService.CreateJwtToken(user);
                        return Ok(token);
                    }
                    return BadRequest("Gick inte att logga in användaren");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal Server error");
            } 
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<bool>> CheckIdentityUserExists(string userName)
        {
            try
            {
                var result = await _aspNetUserService.CheckIdentityUserExists(userName);
                if (!result)
                {
                    return NotFound("Användarnamnet fanns inte registrerat sedan tidigare");
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
