using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;

namespace BankApp2.Web.WebServices
{
    public interface IAccountWebService
    {
        Task<Account> AddAccount(AccountModel accountModel);

    }
}
