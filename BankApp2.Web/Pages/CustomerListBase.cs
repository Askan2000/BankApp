//using BankApp2.Shared.Models;
//using BankApp2.Web.WebServices;
//using Microsoft.AspNetCore.Components;
//using System.Collections.Generic;

//namespace BankApp2.Web.Pages
//{
//    public class CustomerListBase : ComponentBase
//    {
//        //I Blazor-komponenter lägger man inte till en konstruktor för att skapa upp en instans av interfejset, utan gör en inject 
//        [Inject]
//        public ICustomerWebService CustomerWebService { get; set; }

//        public IEnumerable<Customer> Customers { get; set; }

//        protected override async Task OnInitializedAsync()
//        {
//            Customers = (await CustomerWebService.GetCustomers()).ToList();
//        }
//    }
//}
