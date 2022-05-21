using BankApp2.Data.Interfaces;
using BankApp2.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankApp2.Shared.Models;

namespace BankApp2.Data.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly BankAppDataContext _db;

        public CustomerRepo(BankAppDataContext db)
        {
            _db = db;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return result.Entity;        
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _db.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.Transactions)
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.AccountTypes)
                .SingleOrDefaultAsync(c => c.CustomerId == id);

        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            return await _db.Customers
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.Transactions)
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ThenInclude(a => a.AccountTypes)
                .SingleOrDefaultAsync(c => c.AspNetUserId == aspNetId);

        }
    }
}
