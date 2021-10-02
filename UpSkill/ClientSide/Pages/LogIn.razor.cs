namespace UpSkill.ClientSide.Pages
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Account;
    using Microsoft.AspNetCore.Components;
    using UpSkill.ClientSide.Authentication.Services.Contracts;

    public partial class LogIn
    {
        private readonly UserAuthenticationDto userAuthentication = new ();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool AuthErrorExists { get; set; }

        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            AuthErrorExists = false;

            var result = await AuthenticationService
                .Login(this.userAuthentication);

            if (result.AuthIsSuccessful == false)
            {
                Error = result.ErrorMessage;
                AuthErrorExists = true;
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
