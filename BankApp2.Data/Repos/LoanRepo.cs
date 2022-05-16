using BankApp2.Data.Interfaces;
using BankApp2.Data.Models;
using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Repos
{
    public class LoanRepo : ILoanRepo
    {
        private readonly BankAppDataContext _db;

        public LoanRepo(BankAppDataContext db)
        {
            _db = db;
        }

        public async Task<Loan> CreateLoan(Loan loan)
        {
            try
            {
                var result = await _db.Loans.AddAsync(loan);
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
