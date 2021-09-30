namespace UpSkill.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public interface IAccountsService
    {
        public Task<IdentityResult> Register(string fullName, string email, string password, string companyName);

        public Task<bool> Login(string email);

        public bool IsEmailAvailable(string email);

        public bool UserExists(string email);

        public Task<bool> IsPasswordCorrect(string email, string password);
    }
}
