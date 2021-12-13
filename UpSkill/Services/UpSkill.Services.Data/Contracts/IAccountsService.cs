namespace UpSkill.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;

    public interface IAccountsService
    {
        Task<IdentityResult> Register(string fullName, string email, string password, string companyName);

        Task<bool> EmailExists(string email);
        // public bool IsEmailAvailable(string email);

        Task ResetPassword(ApplicationUser user, string emailContent);
    }
}
