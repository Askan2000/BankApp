using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BankApp2.Web.WebServices
{
    public class TransactionWebService : ITransactionWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public TransactionWebService(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
        public async Task<NewTransaction> CreateTransaction(NewTransaction transaction)
        {
            var url = _baseUrl + "api/transaction";

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            string serializedTransaction = JsonConvert.SerializeObject(transaction);

            var token1 = await _sessionStorage.GetItemAsync<string>("token");
            var token2 = token1.Replace("\"", "");

            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token2);

            requestMessage.Content = new StringContent(serializedTransaction);

            requestMessage.Content.Headers.ContentType
                = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseStatusCode = response.StatusCode;
                var responseBody = await response.Content.ReadAsStringAsync();

                var returnedObj = JsonConvert.DeserializeObject<NewTransaction>(responseBody);

                return await Task.FromResult(returnedObj);
            }
            return null;



            //var response = await _httpClient.PostAsJsonAsync(url, transaction);

            //if(response.IsSuccessStatusCode)
            //{
            //    string jsonReturn = await response.Content.ReadAsStringAsync();

            //    NewTransaction returnedTransaction = JsonConvert.DeserializeObject<NewTransaction>(jsonReturn);

            //    return returnedTransaction;
            //}
            //return null;
        }

        public async Task<Transaction> GetTransaction(int accountId)
        {
            var url = _baseUrl + "api/transaction/" + accountId;

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

                return await Task.FromResult(JsonConvert.DeserializeObject<Transaction>(responseBody));


                //var result = await _httpClient.GetFromJsonAsync<Transaction>(url);

                //return result;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
