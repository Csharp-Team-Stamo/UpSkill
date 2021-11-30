namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Dashboard;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Services.Data.Contracts;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Services.Data.Paging;

    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService dashboardService;
        private readonly IEmployeesService employeesService;
        private readonly ICoursesService coursesService;

        public DashboardController( 
            IDashboardService dashboardService,
            IEmployeesService employeesService,
            ICoursesService coursesService)
        {
            this.dashboardService = dashboardService;
            this.employeesService = employeesService;
            this.coursesService = coursesService;
        }

        [HttpGet("GetEpEmployeeDashboardInfoAsync")]
        public async Task<EmployeeDashboardModel> GetEpEmployeeDashboardInfoAsync(string appUserId)
        {
            var employeeId = employeesService.GetEmployeeIdByAppUserId(appUserId);

            return await dashboardService.GetEmployeeDashboardInfoByIdAsync(employeeId);
        }


        [HttpGet("GetOwnerStatistics")]
        public  OwnerDashboardStatisticsModel GetOwnerDashboardStats([FromQuery]string ownerId)
        {
            return dashboardService.GetOwnerDashboardStats(ownerId);
        }

        [HttpGet("GetOwnerCoursesStatistics")]
        public IActionResult GetOwnerCoursesStatistics([FromQuery] string ownerId, string month, [FromQuery] TableEntityParameters parameters)
        {
            var monthAsInt = int.Parse(month);
            var courses = this.coursesService.GetDashboardCourses(ownerId, monthAsInt, parameters);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(courses.MetaData));

            return Ok(courses);
        }
    }
}
