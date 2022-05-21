using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class AspNetUserService : IAspNetUserService
    {

        private readonly IAspNetUserRepo _AspNetUserRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AspNetUserService> _logger;
        public AspNetUserService(IAspNetUserRepo aspNetUserRepo, IMapper mapper, ILogger<AspNetUserService> logger)
        {
            _AspNetUserRepo = aspNetUserRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CheckIdentityUserExists(string userName)
        {
            try
            {
                var result = await _AspNetUserRepo.GetIdentityUser(userName);
                if (result != null)
                {
                    //Användaren finns redan
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CheckIdentityUserExists)} service method {ex} ");
                throw;
            }
        }

        public async Task<ApplicationUser> CreateIdentityAccount(UserRegisterDto user)
        {
            try
            {
                ApplicationUser newUser = new ApplicationUser();

                newUser.UserName = user.Username;
                newUser.Email = user.Email;

                var result = await _AspNetUserRepo.CreateIdentityAccount(newUser, user.Password, user.Role);

                if (result.Succeeded)
                {
                    return newUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateIdentityAccount)} service method {ex} ");
                throw;
            } 
        }

        public async Task<string> GetIdentityUserId(ApplicationUser user)
        {
            try
            {
                var result = await _AspNetUserRepo.GetIdentityUserId(user);

                if (result != null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetIdentityUserId)} service method {ex} ");
                throw;
            }         
        }

        public async Task<ApplicationUser> LoginIdentityAccount(UserLoginDto user)
        {
            try
            {
                var mappedAppUser = _mapper.Map<ApplicationUser>(user);

                return await _AspNetUserRepo.LoginIdentityAccount(mappedAppUser, user.Password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(LoginIdentityAccount)} service method {ex} ");
                throw;
            }
        }
    }
}
