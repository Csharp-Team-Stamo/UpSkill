#nullable enable
namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Employee;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Services.Data.Contracts;
    using Infrastructure.Common.Pagination;

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
        public IActionResult GetCollectionByCompanyId([FromQuery]string companyId, [FromQuery] TableEntityParameters parameters)
        {
            var employees = employeesService.GetByCompanyId(companyId, parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employees.MetaData));

            return Ok(employees);
        }

        [HttpPost("EnrollToCourseAsync")]
        public async Task EnrollToCourseAsync(int courseId, string appUserId)
        {
            var employeeId = employeesService.GetEmployeeIdByAppUserId(appUserId);

           await employeesService.EnrollToCourseAsync(courseId, employeeId);
        }

        [HttpGet("IsEmployeeEnrolledToCourse")]
        public bool IsEmployeeEnrolledToCourse(string appUserId, int courseId)
        {
            var employeeId = employeesService.GetEmployeeIdByAppUserId(appUserId);

            return employeesService.IsEmployeeEnrolledForCourse(employeeId, courseId);
        }

        [HttpGet("GetAchievementsModelAsync")]
        public async Task<EmployeeAchievementsModel> GetAchievementsModelAsync(string userId)
        {
            var employeeId = employeesService.GetEmployeeIdByAppUserId(userId);

            return await employeesService.GetEmployeeAchievementsInfoAsync(employeeId);
        }
    }
}
