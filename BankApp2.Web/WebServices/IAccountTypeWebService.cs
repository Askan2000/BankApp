using BankApp2.Shared.Models;

namespace BankApp2.Web.WebServices
{
    public interface IAccountTypeWebService
    {
        Task<IEnumerable<AccountType>> GetAccountTypes();
    }
}
