namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
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
        public async Task<ActionResult> PostEmployeesCollection(ICollection<AddEmployeeFormModel> employees)
        {
            foreach (var employeeModel in employees)
            {
                var user = new ApplicationUser
                {
                    FullName = employeeModel.FullName,
                    Email = employeeModel.Email,
                    CompanyId = 1,
                    UserName = employeeModel.Email,
                };

                var result = await userManager.CreateAsync(user, "123");

                var emp = new Employee
                {
                    UserId = user.Id,
                };

                await employeeRepository.AddAsync(emp);
            }

            await employeeRepository.SaveChangesAsync();

            return Ok("Weahhh!!");
        }
    }
}
