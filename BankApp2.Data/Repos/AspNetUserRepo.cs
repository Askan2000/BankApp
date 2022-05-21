using BankApp2.Data.Interfaces;
using BankApp2.Data.ModelsIdentity;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Repos
{
    public class AspNetUserRepo : IAspNetUserRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AspNetUserRepo(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> CreateIdentityAccount(ApplicationUser user, string password, string role)
        {           
            var result = await _userManager.CreateAsync(user, password);
            if(result.Succeeded)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, role);
                if(resultRole.Succeeded)
                {
                    return resultRole;
                }
                    
            }
            return result;
        }

        public async Task<IList<string>> GetIdentityRole(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> GetIdentityUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<string> GetIdentityUserId(ApplicationUser user)
        {
            return await _userManager.GetUserIdAsync(user);

        }

        public async Task<ApplicationUser> LoginIdentityAccount(ApplicationUser user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

            if (result.Succeeded) 
            {
                return user;
            }
            return null;
        }
    }
}
