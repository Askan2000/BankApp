using BankApp2.Data.Interfaces;
using BankApp2.Data.Models;
using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankApp2.Data.Repos
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BankAppDataContext _db;

        public TransactionRepo(BankAppDataContext db)
        {
            _db = db;
        }
        public async Task<Transaction> GetTransaction(int accountId)
        {
            var result = await _db.Transactions.Where(t => t.AccountId == accountId).OrderByDescending(x => x.Date).FirstOrDefaultAsync();             
            return result;       
        }

        public async Task<Transaction> PostTransaction(Transaction transaction)
        {
            var result = await _db.Transactions.AddAsync(transaction);
            await _db.SaveChangesAsync();
            return result.Entity;
        }
    }
}
