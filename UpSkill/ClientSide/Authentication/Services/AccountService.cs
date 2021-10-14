namespace UpSkill.ClientSide.Authentication.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Contracts;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Authorization;
    using UpSkill.Infrastructure.Models.Account;

    public class AccountService : IAccountService
    {
        private readonly string clientLoginUrl = "accounts/login";
        private readonly string clientRegisterUrl = "accounts/register";
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly NavigationManager navigationManager;
        private readonly ILocalStorageService localStorage;

        public AccountService(
            HttpClient client, 
            AuthenticationStateProvider authStateProvider, 
            ILocalStorageService localStorage,
            NavigationManager navigationManager)
        {
            this.client = client;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
            this.navigationManager = navigationManager;
        }

        public async Task<RegisterResponseModel> RegisterUser(UserRegisterIM input)
        {
            var content = JsonSerializer.Serialize(input);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await client.PostAsync(this.clientRegisterUrl, bodyContent);

            if (!registrationResult.IsSuccessStatusCode)
            {
                var registrationContent = await registrationResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<RegisterResponseModel>(registrationContent, options);
                return result;
            }

            return new RegisterResponseModel { IsSuccessfulRegistration = true };
        }

        public  async Task<LoginResponseModel> Login(UserLoginIM userData)
        {
            var bodyContent = GetBodyContent(userData);

            var authResult = await PostToHttpClient(this.clientLoginUrl, bodyContent);

            var authResponse = await GetDeserializedAuthResult(authResult);

            if (authResult.IsSuccessStatusCode == false)
            {
                return authResponse;
            }

            await StoreLocally("authToken", authResponse.Token);

            ((UpSkillAuthStateProvider)authStateProvider).NotifyUserAuthentication(authResponse.Token);

            NotifyOfAuthentication(userData.Email);
            SetAuthenticationHeaderForClient("bearer", authResponse.Token);
            
            return new LoginResponseModel { AuthIsSuccessful = true };
        }

        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");

            ((UpSkillAuthStateProvider)this.authStateProvider).NotifyUserLogout();

            this.client.DefaultRequestHeaders.Authorization = null;

            this.navigationManager.NavigateTo("/");
        }

        private async Task<LoginResponseModel> GetDeserializedAuthResult(
            HttpResponseMessage authResult)
        {
            var authContent = await authResult.Content.ReadAsStringAsync();

            return DeserializeAuthContent(authContent, this.options);
        }

        private LoginResponseModel DeserializeAuthContent(
            string authContent, JsonSerializerOptions options)
            => JsonSerializer
                .Deserialize<LoginResponseModel>(authContent, options);

        private async Task<HttpResponseMessage> PostToHttpClient(
            string url, StringContent bodyContent)
            => await this.client.PostAsync(url, bodyContent);

        private void SetAuthenticationHeaderForClient(string key, string value)
            => this.client
                   .DefaultRequestHeaders
                   .Authorization = new AuthenticationHeaderValue(key, value);

        private async Task StoreLocally(string key, string value)
            => await this.localStorage.SetItemAsync(key, value);

        private void NotifyOfAuthentication(string emailAddress)
            => ((UpSkillAuthStateProvider)this.authStateProvider)
                                              .NotifyUserAuthentication(emailAddress);

        private StringContent GetBodyContent(UserLoginIM userData)
        {
            var content = JsonSerializer.Serialize(userData);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
