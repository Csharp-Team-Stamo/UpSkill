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
        private readonly IRepository<Company> companyRepo;

        public AccountsService(UserManager<ApplicationUser> userManager, IRepository<Company> companyRepo)
        {
            this.userManager = userManager;
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
    }
}
