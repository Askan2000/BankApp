using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly HttpContext _httpContext;
        private readonly IAspNetUserRepo _aspNetUserRepo;
        private readonly IMapper _mapper;

        public JwtTokenService(IConfiguration config, IAspNetUserRepo aspNetUserRepo, IMapper mapper)
        {
            _config = config;
            _aspNetUserRepo = aspNetUserRepo;
            _mapper = mapper;
        }

        public async Task<string> CreateJwtToken(UserLoginDto user)
        {
            //Här vill jag lägga till att användaren är en viss roll
            //kan göras via Identity.Roles - först hämta upp vilkan roll personen har i Identity som den fick vid registrering
            //sen sätter jag claimTypes.Role till userns role

            //Hämta upp AspNetAnvändaren och sedan dennes roll
            //var result =  await _aspNetUserRepo.GetIdentityUser();

            var identityUser = await _aspNetUserRepo.GetIdentityUser(user);

            var roles = await _aspNetUserRepo.GetIdentityRole(identityUser);


            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identityUser.Id),
                new Claim(ClaimTypes.Role, roles[0]),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTkey"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenOptions = new JwtSecurityToken(

                issuer: "http://localhost:7019",
                audience: "http://localhost:7019",
                claims: claims, //Här läggs vilken behörighet man har
                expires: DateTime.Now.AddMinutes(20), //Hur länge en token skall gälla
                signingCredentials: credentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }
    }
}
