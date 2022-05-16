using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Identity;
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

        public AspNetUserService(IAspNetUserRepo aspNetUserRepo, IMapper mapper)
        {
            _AspNetUserRepo = aspNetUserRepo;
            _mapper = mapper;
        }

        public async Task<ApplicationUser> CreateIdentityAccount(UserRegisterDto user)
        {

            ApplicationUser newUser = new ApplicationUser();

            newUser.UserName = user.Username;
            newUser.Email = user.Email;

            var result = await _AspNetUserRepo.CreateIdentityAccount(newUser, user.Password, user.Role);

            if(result.Succeeded)
            {
                return newUser;
            }

            return null;
        }

        public async Task<string> GetIdentityUserId(ApplicationUser user)
        {
            var result = await _AspNetUserRepo.GetIdentityUserId(user);

            if( result != null )
            {
                return result;
            }
            return null;
        }

        public async Task<ApplicationUser> LoginIdentityAccount(UserLoginDto user)
        {
            var mappedAppUser = _mapper.Map<ApplicationUser>(user);

            return await _AspNetUserRepo.LoginIdentityAccount(mappedAppUser, user.Password);
        }
    }
}
