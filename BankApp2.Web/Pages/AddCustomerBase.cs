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
        [Parameter]
        public string AddCustomerMessage { get; set; } = "";

        public async Task HandleValidUser()
        {
            //Kolla om användarnamnet redan finns i AspNetIdentity-databasen
            var userNameExists = await CustomerWebService.GetAspNetAccountByUserName(User.Username);

            //Om Username inte redan är upptaget, skapa kontot
            if(!userNameExists)
            {
                var result = await CustomerWebService.CreateCustomer(User);

                if (result != null)
                {
                    NavigationManager.NavigateTo($"/customerSuccesfullyCreated/{result.CustomerId}");
                }
            }
            else
            {
                AddCustomerMessage = "Användarnamnet är redan upptaget";
            }
            
        }
    }
}
