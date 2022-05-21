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
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IMapper _mapper;
        private readonly IAccountTypeRepo _accountTypeRepo;
        private readonly ILogger<AccountTypeService> _logger;

        public AccountTypeService(IAccountTypeRepo accountTypeRepo, IMapper mapper, ILogger<AccountTypeService> logger)
        {
            _accountTypeRepo = accountTypeRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AccountType> CreateAccountType(AccountTypeDto accountType)
        {
            try
            {
                var mappedAccount = _mapper.Map<AccountType>(accountType);
                return await _accountTypeRepo.CreateAccountType(mappedAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateAccountType)} service method {ex} ");
                throw;
            }       
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            try
            {
                return await _accountTypeRepo.GetAccountTypes();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAccountTypes)} service method {ex} ");
                throw;
            }
        }
    }
}
