using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(AccountModel accountDetails);
        Task<Account> UpdateAccount(int accountId, decimal amount);
        Task<Account> GetAccount(int accountId);
    }
}
