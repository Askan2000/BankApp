using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class AddAccountBase : ComponentBase
    {
        public IEnumerable<AccountType> AccountTypes { get; set; } = new List<AccountType>();
        public AccountModel AccountModel { get; set; } = new AccountModel();

        [Inject]
        public IAccountWebService AccountWebService { get; set; }
        [Inject]
        public IAccountTypeWebService AccountTypeWebService { get; set; }
        [Inject]
        public ICustomerWebService CustomerWebService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string AddAcccountMessage { get; set; } = "";

        protected async override Task OnInitializedAsync()
        {
            AccountTypes = (await AccountTypeWebService.GetAccountTypes()).ToList();
        }
        protected async Task HandleValidAccount()
        {
            //Kontrollera om kundId finns registrerat

            var response = await CustomerWebService.GetCustomer(AccountModel.CustomerId);

            if(response != null)
            {
                var result = await AccountWebService.AddAccount(AccountModel);

                if (result != null)
                {
                    NavigationManager.NavigateTo($"SuccesfullAccountRegistration/{result.AccountId}");
                }
            }
            else
            {
                AddAcccountMessage = "Det finns ingen kund med angivet kundId. Försök med ett annat kundId eller lägg upp en ny kund";
            }

        }
    }
}
