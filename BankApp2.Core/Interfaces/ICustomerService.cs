using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int id);

        Task<Customer> GetCustomerByAspNetId(string aspNetId);
        Task<Customer> AddCustomer(UserRegisterDto user, string aspNetUserId);

    }
}
