using Microsoft.AspNetCore.Components;

namespace BankApp2.Web.Shared
{
    public class LoginLogoutButtonBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public ISessionStorageService LocalStorage { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthStateProvider { get; set; }

        protected void Login()
        {
            NavigationManager.NavigateTo("login");
        }

        protected async Task Logout()
        {
            await LocalStorage.RemoveItemAsync("token");
            await AuthStateProvider.GetAuthenticationStateAsync();
        }
    }


}
