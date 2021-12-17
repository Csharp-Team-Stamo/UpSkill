namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Dashboard;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class DashboardService : IDashboardService
    {
        private readonly ICoursesService coursesService;
        private readonly ICoachesService coachesService;
        private readonly IDeletableEntityRepository<CoachOwner> coachOwnerRepository;
        private readonly IDeletableEntityRepository<CourseOwner> courseOwnerRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;

        public DashboardService(
            ICoursesService coursesService,
            ICoachesService coachesService,
            IDeletableEntityRepository<CoachOwner> coachOwnerRepository,
            IDeletableEntityRepository<CourseOwner> courseOwnerRepository,
            IDeletableEntityRepository<Employee> employeeRepository)
        {
            this.coursesService = coursesService;
            this.coachesService = coachesService;
            this.coachOwnerRepository = coachOwnerRepository;
            this.courseOwnerRepository = courseOwnerRepository;
            this.employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDashboardModel> GetEmployeeDashboardInfoByIdAsync(string employeeId)
        {
            return new EmployeeDashboardModel
            {
                Courses = await coursesService.GetAllEnrolledByEmployeeIdAsync(employeeId),
                Coaches = await coachesService.GetAllWithExistingSessionsAsync(employeeId)
            };
        }

        public async Task<OwnerDashboardStatisticsModel> GetOwnerDashboardStats(string ownerId)
        {
            return  new OwnerDashboardStatisticsModel
            {
                Coaches = await this.coachOwnerRepository.AllAsNoTracking().Where(x => x.OwnerId == ownerId).CountAsync(),
                Courses = await this.courseOwnerRepository.AllAsNoTracking().Where(x => x.OwnerId == ownerId).CountAsync(),
                Employees = await this.employeeRepository.AllAsNoTracking().Where(x => x.OwnerId == ownerId).CountAsync()
            };
        }
    }
}
