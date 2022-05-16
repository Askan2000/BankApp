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
        public ICustomerWebService CustomerWebService { get; set; }

        [Inject]
        public ITransactionWebService TransactionWebService { get; set; }

        public Customer Customer { get; set; } = new Customer();
        public NewTransaction NewTransaction { get; set; } = new NewTransaction();

        [Parameter]
        public string Id { get; set; }

        //[Parameter]
        //[Required]
        //public string RecieverAccountId { get; set; } = "";
        //[Parameter]
        //[Required]
        //public string Amount { get; set; } 

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected async override Task OnInitializedAsync()
        {
            //Här behöver jag 

            Customer = await CustomerWebService.GetCustomer(int.Parse(Id));
            //RecieverAccountId = NewTransaction.RecieverAccountId.ToString();
            //Amount = NewTransaction.Amount.ToString();

        }
        protected async Task HandelValidTransaction()
        {
            var result = await TransactionWebService.CreateTransaction(NewTransaction);

            if (result != null)
            {
                NavigationManager.NavigateTo("/succesfulltransaction");
            }
        }
    }
}
