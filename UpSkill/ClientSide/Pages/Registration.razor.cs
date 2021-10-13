namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.ClientSide.Authentication.Services.Contracts;
    using UpSkill.ClientSide.Infrastructure;
    using UpSkill.Infrastructure.Models.Account;

    public partial class Registration
    {
        private readonly UserRegisterIM registerInput = new ();

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public async Task Register()
        {
            ShowRegistrationErrors = false;

            var result = await AccountService.RegisterUser(registerInput);

            if (result.IsSuccessfulRegistration == false)
            {
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            else
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
