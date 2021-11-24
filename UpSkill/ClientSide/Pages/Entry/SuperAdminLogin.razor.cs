namespace UpSkill.ClientSide.Pages.Entry
{
    using System.Threading.Tasks;
    using ClientSide.Authentication.Services.Contracts;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Account;

    public partial class SuperAdminLogin
    {
        private readonly UserAuthenticationModel userAuthentication = new();

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        
        public bool AuthErrorExists { get; set; }

        public string Error { get; set; }

        public async Task ExecuteSuperAdminLogin()
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
