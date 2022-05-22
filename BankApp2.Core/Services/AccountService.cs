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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IMapper mapper, IAccountRepo accountRepo, ILogger<AccountService> logger)
        {
            _mapper = mapper;
            _accountRepo = accountRepo;
            _logger = logger;
        }

        public async Task<Account> CreateAccount(AccountModel accountDetails)
        {
            try
            {
                var mappedAccount = _mapper.Map<Account>(accountDetails);
                return await _accountRepo.CreateAccount(mappedAccount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CreateAccount)} service method {ex} ");
                throw;
            }        
        }

        public async Task<Account> GetAccount(int accountId)
        {
            if(accountId < 1)
                throw new ArgumentOutOfRangeException(nameof(accountId));
            try
            {
                return await _accountRepo.GetAccount(accountId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAccount)} service method {ex} ");
                throw;
            }
        }

        public async Task<Account> UpdateAccount(int accountId, decimal amount)
        {
            if(accountId < 1 || amount < 1)
                throw new ArgumentOutOfRangeException(nameof(accountId));
            try
            {
                return await _accountRepo.UpdateAccount(accountId, amount);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateAccount)} service method {ex} ");
                throw;
            }
        }
    }
}
