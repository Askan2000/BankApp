using BankApp2.Shared.Models;

namespace BankApp2.Web.WebServices
{
    public interface ILoanWebService
    {
        Task<Loan> CreateLoan(Loan loan);
    }
}
