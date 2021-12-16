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
    using Infrastructure.Models.Course;

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
            var employeeId = await employeesService.GetEmployeeIdByAppUserIdAsync(appUserId);

           await employeesService.EnrollToCourseAsync(courseId, employeeId);
        }

        [HttpGet("IsEmployeeEnrolledToCourse")]
        public async Task<bool> IsEmployeeEnrolledToCourse(string appUserId, int courseId)
        {
            var employeeId = await employeesService.GetEmployeeIdByAppUserIdAsync(appUserId);

            return employeesService.IsEmployeeEnrolledForCourse(employeeId, courseId);
        }

        [HttpGet("GetAchievementsModelAsync")]
        public async Task<EmployeeAchievementsModel> GetAchievementsModelAsync(string userId)
        {
            var employeeId = await employeesService.GetEmployeeIdByAppUserIdAsync(userId);

            return await employeesService.GetEmployeeAchievementsInfoAsync(employeeId);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<ActionResult<CourseWatchDetailsModel>> GetCourseById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A valid Id is required");
            }

            var course = await this.employeesService
                .GetCourseById(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }
    }
}
