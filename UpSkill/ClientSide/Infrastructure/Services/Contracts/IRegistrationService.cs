namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Account;

    public interface IRegistrationService
    {
        Task<RegistrationResponseModel> RegisterUser(UserRegistrationModel userForRegistration);
    }
}
