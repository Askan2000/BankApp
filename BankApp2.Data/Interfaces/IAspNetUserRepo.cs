using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Interfaces
{
    public interface IAspNetUserRepo
    {
        Task<IdentityResult> CreateIdentityAccount(ApplicationUser user, string password, string role);
        Task<string> GetIdentityUserId(ApplicationUser user);
        Task<ApplicationUser> LoginIdentityAccount(ApplicationUser user, string password);
        Task<ApplicationUser> GetIdentityUser(string userName);
        Task<IList<string>> GetIdentityRole(ApplicationUser user);

    }
}
