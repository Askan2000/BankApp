using AutoMapper;
using BankApp2.Core.Interfaces;
using BankApp2.Data.Interfaces;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepo repo, IMapper mapper, ILogger<CustomerService> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Customer> AddCustomer(UserRegisterDto user, string aspNetUserId)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));
            if(aspNetUserId == null)
                throw new ArgumentNullException(nameof(aspNetUserId));
            try
            {
                var mappedCustomer = _mapper.Map<Customer>(user);
                mappedCustomer.AspNetUserId = aspNetUserId;

                return await _repo.AddCustomer(mappedCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(AddCustomer)} service method {ex} ");
                throw;
            }
            
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                return await _repo.GetAllCustomers();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllCustomers)} service method {ex} ");
                throw;
            }
        }

        public async Task<Customer> GetCustomer(int id)
        {
            if(id < 1)
                throw new ArgumentOutOfRangeException(nameof(id));
            try
            {
                return await _repo.GetCustomer(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCustomer)} service method {ex} ");
                throw;
            }
        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            try
            {
                return await _repo.GetCustomerByAspNetId(aspNetId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetCustomerByAspNetId)} service method {ex} ");
                throw;
            }
        }
    }
}
