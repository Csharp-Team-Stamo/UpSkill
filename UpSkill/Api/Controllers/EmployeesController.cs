#nullable enable
namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Employee;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService employeesService;

        public EmployeesController(IEmployeesService employeesService)
        {
            this.employeesService = employeesService;
        }

        [HttpPost("PostCollection")]
        public async Task<ActionResult<ICollection<string>>> PostCollection(ICollection<AddEmployeeFormModel> employees)
        {
            var emailsFromErrorResult = await employeesService.SaveEmployeesCollectionAsync(employees);
            return Ok(emailsFromErrorResult);
        }

        [HttpGet("GetCollectionByCompanyId/{companyId}")]
        public ICollection<AddEmployeeFormModel> GetCollectionByCompanyId(string companyId)
        {
            return employeesService.GetByCompanyId(companyId);
        }
    }
}
