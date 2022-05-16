using BankApp2.Shared.ModelsNotInDB;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class AddAccountTypeBase:ComponentBase
    {
        public AccountTypeDto AccountTypeDto { get; set; } = new AccountTypeDto();

        [Inject]
        public IAccountTypeWebService AccountTypeWebService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async void HandleValidAccountType()
        {
            var result = await AccountTypeWebService.CreateAccountType(AccountTypeDto);

            if (result != null)
            {
                NavigationManager.NavigateTo("SuccesfullAccountTypeRegistration");
            }
        }
    }
}
