namespace UpSkill.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;

    public interface IAccountsService
    {
        public Task<IdentityResult> Register(string fullName, string email, string password, string companyName);

        public bool IsEmailAvailable(string email);

        public Task ResetPassword(ApplicationUser user, string emailContent);
    }
}
