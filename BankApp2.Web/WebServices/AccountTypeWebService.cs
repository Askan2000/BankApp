using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BankApp2.Web.WebServices
{
    public class AccountTypeWebService : IAccountTypeWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public AccountTypeWebService(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<AccountType> CreateAccountType(AccountTypeDto accountType)
        {
            var url = _baseUrl + "api/accounttype";

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                string serializedAccountType = JsonConvert.SerializeObject(accountType);

                var token1 = await _sessionStorage.GetItemAsync<string>("token");
                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token2);

                requestMessage.Content = new StringContent(serializedAccountType);

                requestMessage.Content.Headers.ContentType
                    = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedObj = JsonConvert.DeserializeObject<AccountType>(responseBody);

                return await Task.FromResult(returnedObj);

                //if (response.IsSuccessStatusCode)
                //{
                //    string jsonReturn = await response.Content.ReadAsStringAsync();

                //    AccountType newAccountType = JsonConvert.DeserializeObject<AccountType>(jsonReturn);

                //    return newAccountType;
                //}
                //return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            var url = _baseUrl + "api/accounttype/";
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

                return await Task.FromResult(JsonConvert.DeserializeObject<AccountType[]>(responseBody));

                //return await _httpClient.GetFromJsonAsync<AccountType[]>(url);
            }
            catch
            {
                return null;
            }
        }
    }
}
