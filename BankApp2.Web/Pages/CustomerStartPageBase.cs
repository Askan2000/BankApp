using BankApp2.Shared.Models;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class CustomerStartPageBase : ComponentBase
    {
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        public ICustomerWebService CustomerWebService { get; set; }

        public Customer Customer { get; set; }

        [Parameter]
        public string Id { get; set; }
        protected string ButtonBalanceText { get; set; } = "Visa saldo";
        protected string ButtonTransactionsText { get; set; } = "Visa transaktioner";
        protected string CssClassBalance { get; set; } = "Hide";

        protected string CssClassTransactions { get; set; } = "Hide";

        protected async override Task OnInitializedAsync()
        {
            //Här behöver jag ta reda på vilken Customer det är som är inloggad 

            //i min token ligger det info om detta

            var result = await AuthStateProvider.GetAuthenticationStateAsync();
            var aspNetUserId = result.User.Identity.Name;

            if (aspNetUserId != null)
            {
                var response = await CustomerWebService.GetCustomerByAspNetId(aspNetUserId);

                if(response != null)
                {
                    Customer = response;
                }
                else
                {
                    _navigationManager.NavigateTo("Error");
                }
            }
            else
            {
                _navigationManager.NavigateTo("Error");
            }
        }
        protected void Button_Click_Balance()
        {
            if(ButtonBalanceText == "Visa saldo")
            {
                ButtonBalanceText = "Dölj saldo";
                CssClassBalance = null;
            }
            else
            {
                CssClassBalance = "Hide";
                ButtonBalanceText = "Visa saldo";
            }
        }
        protected void Button_Click_Transactions()
        {
            if (ButtonTransactionsText == "Visa transaktioner")
            {
                ButtonTransactionsText = "Dölj transaktioner";
                CssClassTransactions = null;
            }
            else
            {
                CssClassTransactions = "Hide";
                ButtonTransactionsText = "Visa transaktioner";
            }
        }
    }
}
