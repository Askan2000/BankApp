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
            try
            {
                var result = await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                var error = e.StackTrace;
                throw;
            }
            
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            try
            {
                return await _db.Customers
                    .Include(c => c.Dispositions)
                    .ThenInclude(d => d.Account)
                    .ThenInclude(a => a.Transactions)
                    .SingleOrDefaultAsync(c => c.CustomerId == id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            try
            {
                return await _db.Customers
                    .Include(c => c.Dispositions)
                    .ThenInclude(d => d.Account)
                    .ThenInclude(a => a.Transactions)
                    .SingleOrDefaultAsync(c => c.AspNetUserId == aspNetId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
