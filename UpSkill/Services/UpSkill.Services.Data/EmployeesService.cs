namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Identity;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Common;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Services.Data.Paging;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender mailSender;
        private readonly IAccountsService accountsService;
        private readonly IOwnerService ownerService;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, UserManager<ApplicationUser> userManager, IEmailSender mailSender, IAccountsService accountsService, IOwnerService ownerService)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
            this.mailSender = mailSender;
            this.accountsService = accountsService;
            this.ownerService = ownerService;
        }

        public PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, EmployeesParameters parameters)
        {
            var employees =  employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
                new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToList();

           return PagedList<AddEmployeeFormModel>.ToPagedList(employees, parameters.PageNumber, parameters.PageSize);
        }

        public string GetOwnerById(string userId)
        {
            return this.employeeRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).OwnerId;
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

                if (!result.Succeeded)
                {
                    emailsFromErrorResult.AddRange(result.Errors.Select(x => x.Description.Split("'")[1]).ToList());
                }
                else
                {
                    var claimRole = new Claim(ClaimTypes.Role, GlobalConstants.EmployeeRoleName);
                    await userManager.AddClaimAsync(user, claimRole);

                    var emp = new Employee
                    {
                        UserId = user.Id,
                        OwnerId = ownerService.GetId(employeeModel.UserId),
                    };

                    await employeeRepository.AddAsync(emp);
                    await this.accountsService.ResetPassword(user, GlobalConstants.verifyAndResetPassword);
                }
            }

            await employeeRepository.SaveChangesAsync();

            return emailsFromErrorResult;
        }
    }
}
