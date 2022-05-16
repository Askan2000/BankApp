using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Interfaces
{
    public interface IAccountTypeRepo
    {
        Task<IEnumerable<AccountType>> GetAccountTypes();
        Task<AccountType> CreateAccountType(AccountType accountType);

    }
}
