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
        public IActionResult GetCollectionByCompanyId([FromQuery]string companyId, [FromQuery] EmployeesParameters parameters)
        {
            var empployees = employeesService.GetByCompanyId(companyId, parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(empployees.MetaData));

            return Ok(empployees);
        }

        [HttpGet("GetCourseById/{id}")]
        public async Task<ActionResult<EmployeeCourseDetailsModel>> GetCourseById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("A valid Id is required");
            }

            var course = await this.employeesService
                .GetCourseById(id);

            if(course == null)
            {
                return NotFound();
            }

            return course;
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
    }
}
