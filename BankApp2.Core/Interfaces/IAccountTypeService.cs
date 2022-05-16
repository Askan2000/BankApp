using BankApp2.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp2.Core.Interfaces
{
    public interface IAccountTypeService
    {
        Task<IEnumerable<AccountType>> GetAccountTypes();
    }
}
