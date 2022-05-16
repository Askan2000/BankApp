using BankApp2.Shared.ModelsNotInDB;
using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public HttpClient HttpClient { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject]
        public ISessionStorageService SessionStorage { get; set; }

        public UserLoginDto User { get; set; } = new UserLoginDto();

        protected async Task HandleLogin()
        {
            string _baseUrl = "https://localhost:7019/";
            var url = _baseUrl + "api/auth/login";

            //Här får vi tillbaka en jwt-token
            var result = await HttpClient.PostAsJsonAsync(url, User);

            //lite beroende på vad man får tillbaka från apiet sen kanske behöver ändra till readFromJsonAsync eller liknande om objektet är komplext

            var token = result.Content.ReadAsStringAsync();

            //lägg in token i local storage

            await SessionStorage.SetItemAsync("token", token);

            var state = await AuthStateProvider.GetAuthenticationStateAsync();

            var claims = state.User.Claims.ToList();

            var role = "";
            foreach(var claim in claims)
            {
                if (claim.Value.ToUpper() == "CUSTOMER")
                    NavigationManager.NavigateTo("/customerstartpage");
                else if (claim.Value.ToUpper() == "ADMIN")
                    NavigationManager.NavigateTo("/adminstartpage");
                    
            }

        }
    }
}
