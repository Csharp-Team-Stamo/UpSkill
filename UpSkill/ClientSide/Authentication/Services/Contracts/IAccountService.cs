namespace UpSkill.ClientSide.Authentication.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Account;

    public interface IAccountService
    {
        Task<RegisterResponseModel> RegisterUser(UserRegisterIM registerInput);
        Task<LoginResponseModel> Login(UserLoginIM userForAuthentication);
        Task Logout();
    }
}
