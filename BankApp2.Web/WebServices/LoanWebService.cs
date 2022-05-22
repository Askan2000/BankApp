using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BankApp2.Web.WebServices
{
    public class LoanWebService : ILoanWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public LoanWebService(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<Loan> CreateLoan(LoanDto loan)
        {
            var url = _baseUrl + "api/loan";

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                string serializedLoan = JsonConvert.SerializeObject(loan);

                var token1 = await _sessionStorage.GetItemAsync<string>("token");
                var token2 = token1.Replace("\"", "");

                requestMessage.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token2);

                requestMessage.Content = new StringContent(serializedLoan);

                requestMessage.Content.Headers.ContentType
                    = new MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    var responseStatusCode = response.StatusCode;
                    var responseBody = await response.Content.ReadAsStringAsync();

                    var returnedObj = JsonConvert.DeserializeObject<Loan>(responseBody);

                    return await Task.FromResult(returnedObj);
                }
                return null;

                //var response = await _httpClient.PostAsJsonAsync(url, loan);

                //if (response.IsSuccessStatusCode)
                //{
                //    string jsonReturn = await response.Content.ReadAsStringAsync();

                //    Loan newLoan = JsonConvert.DeserializeObject<Loan>(jsonReturn);

                //    return newLoan;
                //}
                //return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
