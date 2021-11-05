namespace UpSkill.ClientSide.Authentication.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Account;

    public interface IAuthenticationService
    {
        Task<UserAuthenticationResponseModel> Login(UserAuthenticationModel userForAuthentication);
        Task Logout();
    }
}
