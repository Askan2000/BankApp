using BankApp2.Data.Interfaces;
using BankApp2.Data.Models;
using BankApp2.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Repos
{
    public class AccountTypeRepo : IAccountTypeRepo
    {
        private readonly BankAppDataContext _db;

        public AccountTypeRepo(BankAppDataContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            try
            {
                return await _db.AccountTypes.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
