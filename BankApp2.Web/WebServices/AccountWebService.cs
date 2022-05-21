using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;

namespace BankApp2.Web.WebServices
{
    public class AccountWebService : IAccountWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;

        public AccountWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Account> AddAccount(AccountModel accountModel)
        {
            var url = _baseUrl + "api/account";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, accountModel);

                if (response.IsSuccessStatusCode)
                {
                    string jsonReturn = await response.Content.ReadAsStringAsync();

                    Account newAccount = JsonConvert.DeserializeObject<Account>(jsonReturn);

                    return newAccount;
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
                return await _httpClient.GetFromJsonAsync<Account>(url);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
