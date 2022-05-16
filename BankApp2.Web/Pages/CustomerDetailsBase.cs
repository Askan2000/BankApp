//using BankApp2.Shared.Models;
//using BankApp2.Web.WebServices;
//using Microsoft.AspNetCore.Components;

//namespace BankApp2.Web.Pages
//{
//    public class CustomerDetailsBase : ComponentBase
//    {
//        public Customer Customer { get; set; } = new Customer();

//        [Inject]
//        public ICustomerWebService? CustomerWebService { get; set; }

//        [Parameter]
//        public string Id { get; set; }

//        protected async override Task OnInitializedAsync()
//        {
//            Id = Id ?? "1";
//            Customer = await CustomerWebService.GetCustomer(int.Parse("1"));
//        }
//    }
//}
