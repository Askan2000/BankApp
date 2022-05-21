using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;

namespace BankApp2.Web.WebServices
{
    public interface ITransactionWebService
    {
        Task<NewTransaction> CreateTransaction(NewTransaction transaction);
        Task<Transaction> GetTransaction(int accountId);

    }
}
