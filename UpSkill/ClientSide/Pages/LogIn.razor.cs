namespace UpSkill.ClientSide.Pages
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.ClientSide.Authentication.Services.Contracts;
    using UpSkill.Infrastructure.Models.Account;

    public partial class LogIn
    {
        private readonly UserLoginIM loginInput = new ();

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool AuthErrorExists { get; set; }

        public string Error { get; set; }

        public async Task ExecuteLogin()
        {
            AuthErrorExists = false;

            var result = await AccountService
                .Login(this.loginInput);

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
