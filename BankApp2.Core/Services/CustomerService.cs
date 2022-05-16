using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _repo;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Customer> AddCustomer(UserRegisterDto user, string aspNetUserId)
        {
            //Customer customer = new Customer();
            var mappedCustomer = _mapper.Map<Customer>(user);
            mappedCustomer.AspNetUserId = aspNetUserId;

            return await _repo.AddCustomer(mappedCustomer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _repo.GetAllCustomers();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _repo.GetCustomer(id);
        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            return await _repo.GetCustomerByAspNetId(aspNetId);
        }
    }
}
