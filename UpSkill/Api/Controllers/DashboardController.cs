namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Dashboard;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService dashboardService;
        private readonly IEmployeesService employeesService;

        public DashboardController( IDashboardService dashboardService,
            IEmployeesService employeesService)
        {
            this.dashboardService = dashboardService;
            this.employeesService = employeesService;
        }

        [HttpGet("GetEpEmployeeDashboardInfoAsync")]
        public async Task<EmployeeDashboardModel> GetEpEmployeeDashboardInfoAsync(string appUserId)
        {
            var employeeId = employeesService.GetEmployeeIdByAppUserId(appUserId);

            return await dashboardService.GetEmployeeDashboardInfoByIdAsync(employeeId);
        }
    }
}
