using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Account;

namespace UpSkill.ClientSide.Infrastructure
{
    public interface IRegistrationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userForRegistration);
    }
}
