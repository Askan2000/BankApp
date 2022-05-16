using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;

namespace BankApp2.Web.WebServices
{
    public class CustomerWebService : ICustomerWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;

        public CustomerWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var url = _baseUrl + "api/customer/" + id;

            try
            {
                return await _httpClient.GetFromJsonAsync<Customer>(url);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            var url = _baseUrl + "api/customer/identity/" + aspNetId;

            try
            {
                return await _httpClient.GetFromJsonAsync<Customer>(url);
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var url = _baseUrl + "api/customer/";
            try
            {
                //HttpResponseMessage response = await _httpClient.GetAsync(url);


                return await _httpClient.GetFromJsonAsync<Customer[]>(url);

                

                //response.EnsureSuccessStatusCode();

                //var json = await response.Content.ReadAsStringAsync();

                //Customer customer = JsonConvert.DeserializeObject<Customer>(json);

                //return customer;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<Customer> CreateCustomer(UserRegisterDto customerDetails)
        {
            var url = _baseUrl + "api/auth/register";

            //string json = JsonConvert.SerializeObject(customerDetails);

            var response = await _httpClient.PostAsJsonAsync(url, customerDetails);

            if (response.IsSuccessStatusCode)
            {
                string jsonReturn = await response.Content.ReadAsStringAsync();

                Customer newCustomer = JsonConvert.DeserializeObject<Customer>(jsonReturn);

                return newCustomer;
            }
            return null;



        }
    }
}
