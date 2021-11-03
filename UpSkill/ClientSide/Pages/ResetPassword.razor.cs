namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Newtonsoft.Json;
    using UpSkill.Infrastructure.Models.Account;

    public partial class ResetPassword
    {
        private readonly UserConfirmPassRequestModel model = new();

        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public async Task ConfirmPassword()
        {
            ShowRegistrationErrors = false;
            var response = await Client.PostAsJsonAsync("/Accounts/Reset-password", model);

            if (!response.IsSuccessStatusCode)
            {
                var resultAsString = response.Content.ReadAsStringAsync().Result;
                var errors = JsonConvert.DeserializeObject<List<string>>(resultAsString);

                Errors = errors;
                ShowRegistrationErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/Login");
            }
        }
    }
}
