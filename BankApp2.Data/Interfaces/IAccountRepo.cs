using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<Account> GetAccount(int id);
        Task<Account> CreateAccount (Account account);
        Task<Account> UpdateAccount (int accountId, decimal newBalance);
    }
}
