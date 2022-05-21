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
        public NavigationManager NavigationManager { get; set; }

        //Lägg upp konto. Är fristående från kunden än så länge

        //Kanske ha en prop här som heter kundId så att kontot kopplas till en Dispositions på direkten, men det kanske blir knas också?

        //I annat fall fulvarianten att man får kolla upp vad det blev för kontonummer och customerId och sen ha en nav-menu option
        //som är att koppla kund till konto -> finns en prop i Dispositions som är Type -> den kan jag automatidskt sätta till "OWNER", står inget
        //i uppgiften om att vi ska hantera detta annorlunda

        //Här skulle man annnars också kanske kunna ha childcomponents

        protected async override Task OnInitializedAsync()
        {
            AccountTypes = (await AccountTypeWebService.GetAccountTypes()).ToList();
        }
        protected async void HandleValidAccount()
        {
            var result = await AccountWebService.AddAccount(AccountModel);

            if(result != null)
            {
                NavigationManager.NavigateTo($"SuccesfullAccountRegistration/{result.AccountId}");
            }
        }
    }
}
