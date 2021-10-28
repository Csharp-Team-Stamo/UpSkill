#nullable enable
namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.AddEmployeeModal;
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
            var errors = await employeesService.SaveEmployeesCollectionAsync(employees);

            return Ok(errors);
        }

        [HttpGet("GetCollectionByCompanyId/{companyId}")]
        public ICollection<AddEmployeeFormModel> GetCollectionByCompanyId(string companyId)
        {
            return employeesService.GetByCompanyId(companyId);
        }
    }
}
