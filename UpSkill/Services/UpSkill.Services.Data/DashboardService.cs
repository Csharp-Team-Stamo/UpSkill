namespace UpSkill.Services.Data
{
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Dashboard;

    public class DashboardService : IDashboardService
    {
        private readonly ICoursesService coursesService;

        public DashboardService(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public async Task<EmployeeDashboardModel> GetEmployeeDashboardInfoByIdAsync(string employeeId)
        {
            return new EmployeeDashboardModel
            {
                Courses = await coursesService.GetAllEnrolledByEmployeeIdAsync(employeeId),
            };
        }
    }
}
