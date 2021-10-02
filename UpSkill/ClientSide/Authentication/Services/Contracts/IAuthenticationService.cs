namespace UpSkill.ClientSide.Authentication.Services.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Account;

    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userForRegistration);
        Task<AuthenticationResponseDto> Login(UserAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
