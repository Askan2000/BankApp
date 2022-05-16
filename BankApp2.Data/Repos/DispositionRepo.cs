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
    public class DispositionRepo : IDispositionRepo
    {
        private readonly BankAppDataContext _db;

        public DispositionRepo(BankAppDataContext db)
        {
            _db = db;
        }

        public async Task<Disposition> CreateDisposition(Disposition disposition)
        {
            try
            {
                var result = await _db.AddAsync(disposition);
                await _db.SaveChangesAsync();
                return disposition;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
