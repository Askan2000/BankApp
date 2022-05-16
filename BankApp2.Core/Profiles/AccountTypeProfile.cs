using AutoMapper;
using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Profiles
{
    public class AccountTypeProfile : Profile
    {
        public AccountTypeProfile()
        {
            CreateMap<AccountTypeDto, AccountType>();
        }
    }
}
