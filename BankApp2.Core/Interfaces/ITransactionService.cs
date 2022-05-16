using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<NewTransaction> CreateTransaction(NewTransaction transaction);
        Task<Transaction> GetTransaction(int accountId);

    }
}
