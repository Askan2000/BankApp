using BankApp2.Shared.Models;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class AddLoanBase : ComponentBase
    {
        public List<int> LoanDuration { get; set; } = new List<int> { 12, 24, 36, 48};
        public Loan Loan { get; set; } = new Loan();
        [Inject]
        public ILoanWebService LoanWebService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        //protected async override Task OnInitializedAsync()
        //{
        //    LoanDuration = 
        //}
        protected async void HandleValidLoan()
        {
            var result = await LoanWebService.CreateLoan(Loan);

            if (result != null)
            {
                NavigationManager.NavigateTo("SuccesfullLoanRegistration");
            }
        }
    }
}
