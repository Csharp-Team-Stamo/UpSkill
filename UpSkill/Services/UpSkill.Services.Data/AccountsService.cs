namespace UpSkill.Services.Data
{
    using Microsoft.AspNetCore.Identity;

    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Common;
    using Contracts;
   

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

            var result = await this.userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return result;
            }

            var claims = new List<Claim>();
            var claimRole = new Claim(ClaimTypes.Role, GlobalConstants.BusinessOwnerRoleName);
            claims.Add(claimRole);

            // TODO Add Company Claim ?

            await this.userManager.AddClaimsAsync(user, claims);
            return result;
        }
    }
}
