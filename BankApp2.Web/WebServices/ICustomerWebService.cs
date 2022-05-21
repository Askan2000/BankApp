using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System.Collections;

namespace BankApp2.Web.WebServices
{
    public interface ICustomerWebService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> GetCustomerByAspNetId(string apNetId);
        Task<Customer> CreateCustomer(UserRegisterDto customerDetails);
        Task<bool> GetAspNetAccountByUserName(string userName);
    }
}
