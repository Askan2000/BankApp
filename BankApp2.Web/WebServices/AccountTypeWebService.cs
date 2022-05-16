using BankApp2.Shared.Models;
using BankApp2.Shared.ModelsNotInDB;
using Newtonsoft.Json;

namespace BankApp2.Web.WebServices
{
    public class AccountTypeWebService : IAccountTypeWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;

        public AccountTypeWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AccountType> CreateAccountType(AccountTypeDto accountType)
        {
            var url = _baseUrl + "api/accounttype";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, accountType);

                if (response.IsSuccessStatusCode)
                {
                    string jsonReturn = await response.Content.ReadAsStringAsync();

                    AccountType newAccountType = JsonConvert.DeserializeObject<AccountType>(jsonReturn);

                    return newAccountType;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<AccountType>> GetAccountTypes()
        {
            var url = _baseUrl + "api/accounttype/";
            try
            {

                return await _httpClient.GetFromJsonAsync<AccountType[]>(url);
            }
            catch
            {
                return null;
            }
        }
    }
}
