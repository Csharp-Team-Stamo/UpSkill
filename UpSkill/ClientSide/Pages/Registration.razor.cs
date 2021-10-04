﻿namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.ClientSide.Infrastructure;
    using UpSkill.Infrastructure.Models.Account;

    public partial class Registration
    {
        private UserRegistrationDto userForRegistration = new UserRegistrationDto();

        [Inject]
        public IRegistrationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
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
