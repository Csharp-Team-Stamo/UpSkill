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

        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public async Task Register()
        {
            ShowRegistrationErrors = false;

            var result = await AuthenticationService.RegisterUser(userForRegistration);
            if (!result.IsSuccessfulRegistration)
            {
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            else
            {
                //TODO Redirect to login page when available
                NavigationManager.NavigateTo("/");
            }
        }

    }
}
