using BankApp2.Shared.ModelsNotInDB;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class AddCustomerBase : ComponentBase
    {
        [Inject]
        public ICustomerWebService CustomerWebService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public UserRegisterDto User { get; set; } = new UserRegisterDto();

        public async void HandleValidUser()
        {
            var result = await CustomerWebService.CreateCustomer(User);

            if (result != null)
            {
                NavigationManager.NavigateTo("/customerSuccesfullyCreated");
            }
        }
    }
}
