using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;
using System.Text;

namespace BankApp2.Web.WebServices
{
    public class TransactionWebService : ITransactionWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;

        public TransactionWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<NewTransaction> CreateTransaction(NewTransaction transaction)
        {
            var url = _baseUrl + "api/transaction";

            var response = await _httpClient.PostAsJsonAsync(url, transaction);

            if(response.IsSuccessStatusCode)
            {
                string jsonReturn = await response.Content.ReadAsStringAsync();

                NewTransaction returnedTransaction = JsonConvert.DeserializeObject<NewTransaction>(jsonReturn);

                return returnedTransaction;
            }
            return null;
        }

        public async Task<Transaction> GetTransaction(int accountId)
        {
            var url = _baseUrl + "api/transaction/" + accountId;

            try
            {
                var result = await _httpClient.GetFromJsonAsync<Transaction>(url);

                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
