using BankApp2.Shared.Models;

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
