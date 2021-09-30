namespace UpSkill.Services.Data
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Services.Data.Contracts;

    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IRepository<Company> companyRepo;

        public AccountsService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRepository<Company> companyRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.companyRepo = companyRepo;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.userManager.Users.Any(x => x.Email == email) == true;
        }

        public async Task<IdentityResult> Register(string fullName, string email, string password, string companyName)
        {
            var company = this.companyRepo
                .All()
                .FirstOrDefault(x => x.Name == companyName);



            if (company == null)
            {
                company = new Company
                {
                    Name = companyName,
                };

                await this.companyRepo.AddAsync(company);
                await this.companyRepo.SaveChangesAsync();
            }


            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FullName = fullName,
                Company = company,
            };

            return await this.userManager.CreateAsync(user, password);
        }

        public bool UserExists(string email)
        {
            return this.userManager.Users.Any(x => x.Email == email);
        }

        public async Task<bool> IsPasswordCorrect(string email, string password)
        {
            var getUser = await this.userManager.FindByEmailAsync(email);

            var result = await this.userManager.CheckPasswordAsync(getUser, password);

            return result;
        }

        public async Task<bool> Login(string email)
        {
            var loggedInUser = await this.userManager.FindByEmailAsync(email);

            await this.signInManager.SignInAsync(loggedInUser, true);

            return true;
        }
    }
}
