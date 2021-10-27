#nullable enable
namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Common.Repositories;
    using Data.Models;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Route("/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public EmployeesController(UserManager<ApplicationUser> userManager, IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.userManager = userManager;
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("PostEmployeesCollection")]
        public async Task<ActionResult<List<string>>> PostEmployeesCollection(ICollection<AddEmployeeFormModel> employees)
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
                    resultErrors.AddRange(result.Errors.Select(x => x.Description.Replace("Username", "Email")).ToList());

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

            return Ok(resultErrors);
        }

        [HttpGet("GetEmployeesByCompanyId/{companyId}")]
        public ActionResult<ICollection<AddEmployeeFormModel>> GetEmployeesByCompanyId(string companyId)
        {
            return employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
                new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToList();
        }
    }
}
