namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Contracts;
    using UpSkill.Infrastructure.Models.Account;

    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public RegistrationService(HttpClient client)
        {
            this.client = client;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<RegistrationResponseModel> RegisterUser(UserRegistrationModel input)
        {
            var content = JsonSerializer.Serialize(input);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await client.PostAsync("accounts/register", bodyContent);

            if (!registrationResult.IsSuccessStatusCode)
            {
                var registrationContent = await registrationResult.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<RegistrationResponseModel>(registrationContent, options);
                return result;
            }

            return new RegistrationResponseModel { IsSuccessfulRegistration = true };
        }
    }
}
