namespace UpSkill.Services.Data
{
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Dashboard;

    public class DashboardService : IDashboardService
    {
        private readonly ICoursesService coursesService;
        private readonly ICoachesService coachesService;

        public DashboardService(
            ICoursesService coursesService,
            ICoachesService coachesService)
        {
            this.coursesService = coursesService;
            this.coachesService = coachesService;
        }

        public async Task<EmployeeDashboardModel> GetEmployeeDashboardInfoByIdAsync(string employeeId)
        {
            return new EmployeeDashboardModel
            {
                Courses = await coursesService.GetAllEnrolledByEmployeeIdAsync(employeeId),
                Coaches = await coachesService.GetAllWithExistingSessions(employeeId)
            };
        }
    }
}
