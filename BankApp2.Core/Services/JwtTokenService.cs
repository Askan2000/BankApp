using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly IAspNetUserRepo _aspNetUserRepo;
        private readonly ILogger<JwtTokenService> _logger;

        public JwtTokenService(IConfiguration config, IAspNetUserRepo aspNetUserRepo, ILogger<JwtTokenService> logger)
        {
            _config = config;
            _aspNetUserRepo = aspNetUserRepo;
            _logger = logger;
        }

        public async Task<string> CreateJwtToken(UserLoginDto user)
        {
            try
            {
                var identityUser = await _aspNetUserRepo.GetIdentityUser(user.Username);

                var roles = await _aspNetUserRepo.GetIdentityRole(identityUser);


                List<Claim> claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, identityUser.Id),
                new Claim(ClaimTypes.Role, roles[0]),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTkey"]));

                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(

                    issuer: "http://localhost:7019",
                    audience: "http://localhost:7019",
                    claims: claims, //Här läggs vilken behörighet man har
                    expires: DateTime.Now.AddMinutes(20), //Hur länge en token skall gälla
                    signingCredentials: credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateJwtToken)} service method {ex} ");
                throw;
            }

            
        }
    }
}
