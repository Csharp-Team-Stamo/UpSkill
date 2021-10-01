namespace UpSkill.ClientSide.Infrastructure
{

    using System;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Account;

    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions _options;

        public RegistrationService(HttpClient client)
        {
            this.client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto input)
        {
            var content = JsonSerializer.Serialize(input);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await client.PostAsync("accounts/register", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, _options);
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
