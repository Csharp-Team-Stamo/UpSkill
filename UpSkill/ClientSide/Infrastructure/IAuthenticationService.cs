using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Account;

namespace UpSkill.ClientSide.Infrastructure
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userForRegistration);
    }
}
