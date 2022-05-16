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
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepo _accountRepo;

        public AccountService(IMapper mapper, IAccountRepo accountRepo)
        {
            _mapper = mapper;
            _accountRepo = accountRepo;
        }

        public async Task<Account> CreateAccount(AccountModel accountDetails)
        {
            var mappedAccount = _mapper.Map<Account>(accountDetails);

            return await _accountRepo.CreateAccount(mappedAccount);
        }

        public async Task<Account> UpdateAccount(int accountId, decimal amount)
        {
            return await _accountRepo.UpdateAccount(accountId, amount);
        }
    }
}
