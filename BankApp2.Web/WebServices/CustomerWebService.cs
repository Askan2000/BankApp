using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BankApp2.Web.WebServices
{
    public class CustomerWebService : ICustomerWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        public  HttpClient _httpClient { get; }

        private readonly ISessionStorageService _sessionStorage;


        public CustomerWebService(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7019/");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var url = _baseUrl + "api/customer/" + id;

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var token1 = await _sessionStorage.GetItemAsync<string>("token");
                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization
                    = new AuthenticationHeaderValue("Bearer", token2);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                return await Task.FromResult(JsonConvert.DeserializeObject<Customer>(responseBody));


                //return await _httpClient.GetFromJsonAsync<Customer>(url);
            }

            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Customer> GetCustomerByAspNetId(string aspNetId)
        {
            try
            {
                var url = "api/customer/identity/" + aspNetId;

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var token1 = await _sessionStorage.GetItemAsync<string>("token");

                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token2);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                return await Task.FromResult(JsonConvert.DeserializeObject<Customer>(responseBody));
            }
            catch (Exception)
            {
                return null;
            }
        }   

        //public async Task<IEnumerable<Customer>> GetCustomers()
        //{
        //    var url = _baseUrl + "api/customer/";
        //    try
        //    {
        //        //HttpResponseMessage response = await _httpClient.GetAsync(url);

        //        return await _httpClient.GetFromJsonAsync<Customer[]>(url);         

        //        //response.EnsureSuccessStatusCode();

        //        //var json = await response.Content.ReadAsStringAsync();

        //        //Customer customer = JsonConvert.DeserializeObject<Customer>(json);

        //        //return customer;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public async Task<Customer> CreateCustomer(UserRegisterDto customerDetails)
        {
            var url = _baseUrl + "api/auth/register";

            string serializedCustomer = JsonConvert.SerializeObject(customerDetails);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
           
            var token1 = await _sessionStorage.GetItemAsync<string>("token");
            var token2 = token1.Replace("\"", "");

            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token2);

            requestMessage.Content = new StringContent(serializedCustomer);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedObj = JsonConvert.DeserializeObject<Customer>(responseBody);

            return await Task.FromResult(returnedObj);

            //if (response.IsSuccessStatusCode)
            //{
            //    string jsonReturn = await response.Content.ReadAsStringAsync();

            //    Customer newCustomer = JsonConvert.DeserializeObject<Customer>(jsonReturn);

            //    return newCustomer;
            //}
            //return null;
        }

        public async Task<bool> GetAspNetAccountByUserName(string userName)
        {
            var url = _baseUrl + "api/auth/" + userName;
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                var token1 = await _sessionStorage.GetItemAsync<string>("token");
                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization
                    = new AuthenticationHeaderValue("Bearer", token2);

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                return await Task.FromResult(JsonConvert.DeserializeObject<bool>(responseBody));


                //return await _httpClient.GetFromJsonAsync<bool>(url);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
