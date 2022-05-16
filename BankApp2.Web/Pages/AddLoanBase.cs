using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class AddLoanBase : ComponentBase
    {
        public List<int> LoanDuration { get; set; } = new List<int> { 12, 24, 36, 48};
        public LoanDto LoanDto { get; set; } = new LoanDto();
        [Inject]
        public ILoanWebService LoanWebService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async void HandleValidLoan()
        {
            var result = await LoanWebService.CreateLoan(LoanDto);

            if (result != null)
            {
                NavigationManager.NavigateTo("SuccesfullLoanRegistration");
            }
        }
    }
}
