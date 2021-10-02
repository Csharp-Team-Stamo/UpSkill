namespace UpSkill.ClientSide.Authentication.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Contracts;
    using Microsoft.AspNetCore.Components.Authorization;
    using UpSkill.Infrastructure.Models.Account;

    public class AuthenticationService : IAuthenticationService
    {
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
            UserAuthenticationDto userAuthenticationDto)
        {
            var content = JsonSerializer.Serialize(userAuthenticationDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await this.client.PostAsync("accounts/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthenticationResponseDto>(authContent, this.options);
            if (!authResult.IsSuccessStatusCode)
                return result;
            await this.localStorage.SetItemAsync("authToken", result.Token);
            ((UpSkillAuthStateProvider)this.authStateProvider).NotifyUserAuthentication(userAuthenticationDto.Email);
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthenticationResponseDto { AuthIsSuccessful = true };
        }

        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");
            ((UpSkillAuthStateProvider)this.authStateProvider).NotifyUserLogout();
            this.client.DefaultRequestHeaders.Authorization = null;
        }
    }
}
