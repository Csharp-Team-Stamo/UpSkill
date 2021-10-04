namespace UpSkill.ClientSide.Authentication.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Contracts;
    using Infrastructure.Models.Account;
    using Microsoft.AspNetCore.Components.Authorization;

    public class AuthenticationService : IAuthenticationService
    {
        private readonly string clientUrl = "accounts/login";
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(
            HttpClient client, 
            AuthenticationStateProvider authStateProvider, 
            ILocalStorageService localStorage)
        {
            this.client = client;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }
        public  async Task<AuthenticationResponseDto> Login(
            UserAuthenticationDto userData)
        {
            var bodyContent = GetBodyContent(userData);

            var authResult = await PostToHttpClient(this.clientUrl, bodyContent);

            var authResponse = await GetDeserializedAuthResult(authResult);

            if (authResult.IsSuccessStatusCode == false)
            {
                return authResponse;
            }

            await StoreLocally("authToken", authResponse.Token); 
            
            NotifyOfAuthentication(userData.Email);

            SetAuthenticationHeaderForClient("bearer", authResponse.Token);
            
            return new AuthenticationResponseDto { AuthIsSuccessful = true };
        }

        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");

            ((UpSkillAuthStateProvider)this.authStateProvider).NotifyUserLogout();

            this.client.DefaultRequestHeaders.Authorization = null;
        }

        private async Task<AuthenticationResponseDto> GetDeserializedAuthResult(
            HttpResponseMessage authResult)
        {
            var authContent = await authResult.Content.ReadAsStringAsync();

            return DeserializeAuthContent(authContent, this.options);
        }

        private AuthenticationResponseDto DeserializeAuthContent(
            string authContent, JsonSerializerOptions options)
            => JsonSerializer
                .Deserialize<AuthenticationResponseDto>(authContent, options);

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

        private StringContent GetBodyContent(UserAuthenticationDto userData)
        {
            var content = JsonSerializer.Serialize(userData);
            return new StringContent(content, Encoding.UTF8, "application/json");
        }
    }
}
