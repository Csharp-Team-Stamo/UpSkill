namespace UpSkill.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IAccountsService
    {
        public Task<IdentityResult> Register(string fullName, string email, string password, string companyName);

        public bool IsEmailAvailable(string email);
    }
}
