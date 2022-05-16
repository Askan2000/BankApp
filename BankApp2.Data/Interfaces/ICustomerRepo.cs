using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Data.Interfaces
{
    public interface ICustomerRepo
    {
        Task<IEnumerable <Customer>> GetAllCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> GetCustomerByAspNetId(string aspNetId);
        Task<Customer> AddCustomer(Customer customer);
    }
}
