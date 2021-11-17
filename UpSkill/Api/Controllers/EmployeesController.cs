#nullable enable
namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.AddEmployeeModal;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Services.Data.Contracts;
    using UpSkill.Infrastructure.Common.Pagination;

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

        [HttpGet("GetCollectionByCompanyId")]
        public IActionResult GetCollectionByCompanyId([FromQuery]string companyId, [FromQuery] EmployeesParameters parameters)
        {
            var empployees = employeesService.GetByCompanyId(companyId, parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(empployees.MetaData));

            return Ok(empployees);
           
        }
    }
}
