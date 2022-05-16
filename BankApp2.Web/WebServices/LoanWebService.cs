using BankApp2.Shared.Models;
using Newtonsoft.Json;

namespace BankApp2.Web.WebServices
{
    public class LoanWebService : ILoanWebService
    {
        private readonly string _baseUrl = "https://localhost:7019/";

        private readonly HttpClient _httpClient;

        public LoanWebService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Loan> CreateLoan(Loan loan)
        {
            var url = _baseUrl + "api/loan";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, loan);

                if (response.IsSuccessStatusCode)
                {
                    string jsonReturn = await response.Content.ReadAsStringAsync();

                    Loan newLoan = JsonConvert.DeserializeObject<Loan>(jsonReturn);

                    return newLoan;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
