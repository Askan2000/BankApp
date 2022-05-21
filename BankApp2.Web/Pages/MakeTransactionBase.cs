using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using BankApp2.Web.WebServices;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BankApp2.Web.Pages
{
    public class MakeTransactionBase : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject]
        public ICustomerWebService CustomerWebService { get; set; }

        [Inject]
        public ITransactionWebService TransactionWebService { get; set; }

        public Customer Customer { get; set; }
        public NewTransaction NewTransaction { get; set; } = new NewTransaction();

        [Parameter]
        public string Id { get; set; }
        public string TransactionMessage { get; set; } = "";

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var result = await AuthStateProvider.GetAuthenticationStateAsync();
            var aspNetUserId = result.User.Identity.Name;

            if (aspNetUserId != null)
            {
                var response = await CustomerWebService.GetCustomerByAspNetId(aspNetUserId);

                if (response != null)
                {
                    Customer = response;
                }
                else
                {
                    NavigationManager.NavigateTo("Error");
                }
            }
            else
            {
                NavigationManager.NavigateTo("Error");
            }
        }
        protected async Task HandelValidTransaction()
        {
            //Kolla om det finns tillräckligt med Balance på avsändarens konto för att genomföra transaktionen

            var lastTransactionSender = await TransactionWebService.GetTransaction(int.Parse(NewTransaction.SenderAccountId));
            //var lastTransactionReciever = await TransactionWebService.GetTransaction(int.Parse(NewTransaction.RecieverAccountId));

            if (lastTransactionSender != null)
            {
                //Om det finns pengar på avsändarens konto, kolla om det är tillräckligt för att göra överföringen
                var balanceAfterTransaction = lastTransactionSender.Balance - int.Parse(NewTransaction.Amount);
                if(balanceAfterTransaction > 0)
                {
                    var result = await TransactionWebService.CreateTransaction(NewTransaction);

                    if (result != null)
                    {
                        NavigationManager.NavigateTo("/succesfulltransaction");
                    }
                }
                else
                {
                    TransactionMessage = "Du har inte tillräckligt med pengar på kontot för överföringen";
                }
            }
            else
            {
                TransactionMessage = "Du har inte tillräckligt med pengar på kontot för överföringen";
            }
        }
    }
}
