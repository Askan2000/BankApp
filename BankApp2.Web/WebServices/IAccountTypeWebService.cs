using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;

namespace BankApp2.Web.WebServices
{
    public interface IAccountTypeWebService
    {
        Task<IEnumerable<AccountType>> GetAccountTypes();
        Task<AccountType> CreateAccountType(AccountTypeDto accountType);
    }
}
