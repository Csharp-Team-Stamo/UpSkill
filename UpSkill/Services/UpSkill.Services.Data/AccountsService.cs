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
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Company> companyRepo;
        private readonly IEmailSender mailSender;
        private readonly IDeletableEntityRepository<Owner> ownerRepository;
        private readonly IConfiguration configuration;

        public AccountsService(UserManager<ApplicationUser> userManager, IRepository<Company> companyRepo, IEmailSender mailSender, IDeletableEntityRepository<Owner> ownerRepository, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.companyRepo = companyRepo;
            this.mailSender = mailSender;
            this.ownerRepository = ownerRepository;
            this.configuration = configuration;
        }

        public async Task<bool> EmailExists(string email)
            => await this.userManager.FindByEmailAsync(email) != null;

        //public bool IsEmailAvailable(string email)
        //{
        //    return !this.userManager.Users.Any(x => x.Email == email) == true;
        //}

        public async Task<IdentityResult> Register(string fullName, string email, string password, string companyName)
        {
            var company = await this.companyRepo
                .All()
                .FirstOrDefaultAsync(x => x.Name == companyName);

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

            var owner = new Owner { UserId = user.Id, };
            await ownerRepository.AddAsync(owner);
            await ownerRepository.SaveChangesAsync();

            var claims = new List<Claim>();
            var claimRole = new Claim(ClaimTypes.Role, GlobalConstants.BusinessOwnerRoleName);
            claims.Add(claimRole);

            await this.userManager.AddClaimsAsync(user, claims);
            return result;
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = true,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public async Task ResetPassword(ApplicationUser user, string emailContent)
        {
            //TODO 
                var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetUrl = this.configuration["ClientSide:URL"] + "/reset-password?email=" + user.Email + "&token=" + passwordResetToken;

                var content = string.Format(emailContent, user.FullName, passwordResetUrl);

                await this.mailSender.SendMailAsync("Upskill: Reset password requested!", user.Email, user.FullName, content, content);
        }
    }
}
