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
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IMapper _mapper;
        private readonly IAccountTypeRepo _accountTypeRepo;

        public AccountTypeService(IAccountTypeRepo accountTypeRepo, IMapper mapper)
        {
            _accountTypeRepo = accountTypeRepo;
            _mapper = mapper;
        }

        public async Task<AccountType> CreateAccountType(AccountTypeDto accountType)
        {
            var mappedAccount = _mapper.Map<AccountType>(accountType);

            return await _accountTypeRepo.CreateAccountType(mappedAccount);
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            return await _accountTypeRepo.GetAccountTypes();
        }
    }
}
