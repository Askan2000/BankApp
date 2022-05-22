using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BankApp2.Web.WebServices
{
    public class AccountWebService : IAccountWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public AccountWebService(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<Account> AddAccount(AccountModel accountModel)
        {
            var url = _baseUrl + "api/account";

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                var serializedAccount = JsonConvert.SerializeObject(accountModel);

                var token1 = await _sessionStorage.GetItemAsync<string>("token");
                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token2);

                requestMessage.Content = new StringContent(serializedAccount);

                requestMessage.Content.Headers.ContentType
                    = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var responseStatusCode = response.StatusCode;
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var returnedObj = JsonConvert.DeserializeObject<Account>(responseBody);

                    return await Task.FromResult(returnedObj);
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<Account> GetAccount(int accountId)
        {
            var url = _baseUrl + "api/account/" + accountId;

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

                return await Task.FromResult(JsonConvert.DeserializeObject<Account>(responseBody));


                //return await _httpClient.GetFromJsonAsync<Account>(url);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
