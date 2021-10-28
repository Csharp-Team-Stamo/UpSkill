namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Identity;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, UserManager<ApplicationUser> userManager)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
        }

        public ICollection<AddEmployeeFormModel> GetByCompanyId(string companyId)
        {
            return employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
                new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToList();
        }

        public async Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees)
        {
            var resultErrors = new List<string>();

            foreach (var employeeModel in employees)
            {
                var user = new ApplicationUser
                {
                    FullName = employeeModel.FullName,
                    Email = employeeModel.Email,
                    CompanyId = int.Parse(employeeModel.CompanyId),
                    UserName = employeeModel.Email,

                };

                var result = await userManager.CreateAsync(user, "123");

                if (!result.Succeeded)
                {
                    resultErrors.AddRange(result.Errors.Select(x => x.Description.Split("'")[1]).ToList());
                }
                else
                {
                    var emp = new Employee
                    {
                        UserId = user.Id,
                    };

                    await employeeRepository.AddAsync(emp);
                }
            }

            await employeeRepository.SaveChangesAsync();

            return resultErrors;
        }
    }
}
