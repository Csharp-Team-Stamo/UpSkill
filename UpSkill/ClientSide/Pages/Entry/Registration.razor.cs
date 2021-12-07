namespace UpSkill.ClientSide.Pages.Entry
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Services.Contracts;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Account;

    public partial class Registration
    {
        private readonly UserRegistrationModel userForRegistration = new();

        [Inject]
        public IRegistrationService AuthenticationService { get; set; }

        public bool ShowErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public async Task Register()
        {
            ShowErrors = false;

            var result = await AuthenticationService.RegisterUser(userForRegistration);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
            else
            {
                //TODO Redirect to login page when available
                NavigationManager.NavigateTo("/");
            }
        }

    }
}
