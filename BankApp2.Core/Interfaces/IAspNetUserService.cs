using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Interfaces
{
    public interface IAspNetUserService
    {
        Task<ApplicationUser> CreateIdentityAccount(UserRegisterDto user);
        Task<string> GetIdentityUserId(ApplicationUser user);
        Task<ApplicationUser> LoginIdentityAccount(UserLoginDto user);
        Task<bool> CheckIdentityUserExists(string userName);
    }
}
