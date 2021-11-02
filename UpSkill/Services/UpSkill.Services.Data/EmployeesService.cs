namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.WebUtilities;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Common;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender mailSender;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, UserManager<ApplicationUser> userManager, IEmailSender mailSender)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
            this.mailSender = mailSender;
        }

        public ICollection<AddEmployeeFormModel> GetByCompanyId(string companyId)
        {
            return employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
                new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToList();
        }

        public async Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees)
        {
            var emailsFromErrorResult = new List<string>();

            foreach (var employeeModel in employees)
            {
                var user = new ApplicationUser
                {
                    FullName = employeeModel.FullName,
                    Email = employeeModel.Email,
                    CompanyId = int.Parse(employeeModel.CompanyId),
                    UserName = employeeModel.Email,

                };

                var result = await userManager.CreateAsync(user, AccountsService.GenerateRandomPassword());

                var claimRole = new Claim(ClaimTypes.Role, GlobalConstants.EmployeeRoleName);

                await userManager .AddClaimAsync(user, claimRole);

                if (!result.Succeeded)
                {
                    emailsFromErrorResult.AddRange(result.Errors.Select(x => x.Description.Split("'")[1]).ToList());
                }
                else
                {
                    var emp = new Employee
                    {
                        UserId = user.Id,
                    };

                    await employeeRepository.AddAsync(emp);

                    await ResetPassword(user);
                }
            }

            await employeeRepository.SaveChangesAsync();

            return emailsFromErrorResult;
        }

        public async Task ResetPassword(ApplicationUser user)
        {
            var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetUrl = "https://localhost:44363/reset-password?email=" + user.Email + "&token=" + passwordResetToken;

            var content = string.Format(GlobalConstants.verifyAndResetPassword, user.FullName, passwordResetUrl);

            await this.mailSender.SendMailAsync("Upskill: Reset password requested!", user.Email, user.FullName, content, content);
        }

    }
}
