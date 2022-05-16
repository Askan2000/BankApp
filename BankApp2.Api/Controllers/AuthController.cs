using BankApp2.Core.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
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

        [HttpPost("register")]
        public async Task<ActionResult<Customer>> Register (UserRegisterDto user)
        {

            if(user == null)
            {
                BadRequest("Felktiga uppgifter");
            }
            else
            {
                var result = await _aspNetUserService.CreateIdentityAccount(user);

                if(result != null)
                {
                    //Om det gick bra att skapa AspNetUsern vill jag hämta upp denna och lägga in en Customer med AspNetUserId:et på
                    var aspNetUserId = await _aspNetUserService.GetIdentityUserId(result);

                    if(aspNetUserId != null)
                    {
                        var resultCustomer = await _customerService.AddCustomer(user, aspNetUserId);

                        if(resultCustomer != null)
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
            return BadRequest("Något gick fel"); 
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto user)
        {
            //Kolla via login-in-manager om usern finns, ändra sen nedan

            if(user == null)
            {
                return BadRequest("Felaktiga inloggningsuppgifter");
            }
            else
            {
                //prova att logga in användaren i Identity

                var result = await _aspNetUserService.LoginIdentityAccount(user);
                
                if(result != null)
                {
                    string token =  await _jwtTokenService.CreateJwtToken(user);
                    return Ok(token);

                }
                return null;
            }
        }

        //private string CreateToken(UserLoginDto user)
        //{

        //    //Här vill jag lägga till att användaren är en viss roll
        //    //kan göras via Identity.Roles - först hämta upp vilkan roll personen har i Identity som den fick vid registrering
        //    //sen sätter jag claimTypes.Role till userns role

        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Username)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTkey"]));
            
        //    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var tokenOptions = new JwtSecurityToken(

        //        issuer: "http://localhost:7019",
        //        audience: "http://localhost:7019",
        //        claims: claims, //Här läggs vilken behörighet man har
        //        expires: DateTime.Now.AddMinutes(20), //Hur länge en token skall gälla
        //        signingCredentials: credentials);

        //    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //    return tokenString;
        //}
    }
}
