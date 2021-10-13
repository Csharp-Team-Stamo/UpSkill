namespace UpSkill.Services.Data
{
    using Microsoft.AspNetCore.Identity;

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Common;
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

        public bool EmailIsUnavailable(string email)
        {
            return this.userManager.Users.Any(x => x.Email == email);
        }

        public async Task<Company> GetCompany(string companyName)
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

            return company;
        }
    }
}
