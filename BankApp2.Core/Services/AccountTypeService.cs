using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class AccountTypeService : IAccountTypeService
    {

        private readonly IAccountTypeRepo _accountTypeRepo;

        public AccountTypeService(IAccountTypeRepo accountTypeRepo)
        {
            _accountTypeRepo = accountTypeRepo;
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            return await _accountTypeRepo.GetAccountTypes();
        }
    }
}
