namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Employee;
    using Microsoft.AspNetCore.Identity;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Common;
    using Infrastructure.Common.Pagination;
    using Microsoft.EntityFrameworkCore;
    using Paging;
    using Infrastructure.Models.Course;
    using Infrastructure.Models.Lecture;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountsService accountsService;
        private readonly IOwnerService ownerService;
        private readonly IDeletableEntityRepository<EmployeeCourse> employeeCourseRepository;
        private readonly IDeletableEntityRepository<Coach> coachRepository;
        private readonly IDeletableEntityRepository<Course> courseRepo;

        public EmployeesService(
            IDeletableEntityRepository<Employee> employeeRepository,
            UserManager<ApplicationUser> userManager,
            IAccountsService accountsService,
            IOwnerService ownerService,
            IDeletableEntityRepository<EmployeeCourse> employeeCourseRepository,
            IDeletableEntityRepository<Coach> coachRepository,
            IDeletableEntityRepository<Course> courseRepo)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
            this.accountsService = accountsService;
            this.ownerService = ownerService;
            this.employeeCourseRepository = employeeCourseRepository;
            this.coachRepository = coachRepository;
            this.courseRepo = courseRepo;
        }

        public async Task<string> GetEmployeeIdByAppUserIdAsync(string userId)
        {
            return  await this.employeeRepository.All().Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<PagedList<AddEmployeeFormModel>> GetByCompanyId(string companyId,
            TableEntityParameters parameters)
        {
            var employees = await employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
               new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToListAsync();

            return PagedList<AddEmployeeFormModel>.ToPagedList(employees, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<EmployeeAchievementsModel> GetEmployeeAchievementsInfoAsync(string employeeId)
        {
            return new EmployeeAchievementsModel
            {
                Courses = await employeeCourseRepository.All().Where(x => x.StudentId == employeeId).Select(x => new CourseInEmployeeAchievementsModel
                {
                    CourseName = x.Course.Name,
                    EndDate = x.EndDate.ToString("d"),
                    StartDate = x.EnrollDate.ToString("d"),
                    Grade = x.Grade == null ? "Pending" : x.Grade.ToString(),
                }).ToListAsync(),
                Coaches = await coachRepository.All()
                    .Where(x => x.LiveSessions.Any(ls => ls.StudentId == employeeId && ls.End < DateTime.Now))
                    .Select(x => new CoachInEmployeeAchievementsModel
                    {
                        CoachName = x.FullName,
                        FirstLiveSessionDate = x.LiveSessions
                            .Where(x => x.StudentId == employeeId)
                            .OrderBy(x => x.End)
                            .FirstOrDefault()
                            .End
                            .ToString("d"),
                        LastLiveSessionDate = x.LiveSessions
                            .Where(x => x.StudentId == employeeId)
                            .OrderByDescending(x => x.End)
                            .FirstOrDefault()
                            .End.
                            ToString("d"),
                        SessionsCompleted = x.LiveSessions.Count(liveSession => liveSession.StudentId == employeeId).ToString(),
                    }).ToListAsync(),
            };
        }

        public async Task<string> GetOwnerIdByAppUserId(string userId)
        {
            return await this.employeeRepository.AllAsNoTracking().Where(x => x.UserId == userId).Select(x => x.OwnerId).FirstOrDefaultAsync();
        }

        public async Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees)
        {
            var emailsFromErrorResult = new List<string>();

            foreach (var employeeModel in employees)
            {
                var user = new ApplicationUser
                {
                    FullName = employeeModel.FullName,
                    Email = employeeModel.Email,
                    CompanyId = int.Parse(employeeModel.CompanyId),
                    UserName = employeeModel.Email,
                };

                var result = await userManager.CreateAsync(user, AccountsService.GenerateRandomPassword());

                if (!result.Succeeded)
                {
                    emailsFromErrorResult.AddRange(result.Errors.Select(x => x.Description.Split("'")[1]).ToList());
                }
                else
                {
                    var claimRole = new Claim(ClaimTypes.Role, GlobalConstants.EmployeeRoleName);
                    await userManager.AddClaimAsync(user, claimRole);

                    var emp = new Employee
                    {
                        UserId = user.Id,
                        OwnerId = await ownerService.GetId(employeeModel.UserId),
                    };

                    await employeeRepository.AddAsync(emp);
                    await this.accountsService.ResetPassword(user, GlobalConstants.verifyAndResetPassword);
                }
            }

            await employeeRepository.SaveChangesAsync();

            return emailsFromErrorResult;
        }

        public async Task EnrollToCourseAsync(int courseId, string employeeId)
        {
            if (!employeeCourseRepository.All().Any(x => x.CourseId == courseId && x.StudentId == employeeId))
            {
                var employeeCourse = new EmployeeCourse { CourseId = courseId, StudentId = employeeId };
                await employeeCourseRepository.AddAsync(employeeCourse);
                await employeeCourseRepository.SaveChangesAsync();
            }
        }

        public bool IsEmployeeEnrolledForCourse(string employeeId, int courseId)
        {
            return employeeCourseRepository.All()
                .Any(x => x.StudentId == employeeId && x.CourseId == courseId);
        }

        public async Task<CourseWatchDetailsModel> GetCourseById(int id)
        {
            var courseDetails = await this.courseRepo
                .All()
                .Select(c => new CourseWatchDetailsModel
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    AuthorFullName = c.AuthorFullName,
                    AuthorImageUrl = c.AuthorImageUrl,
                    AuthorCompanyLogoUrl = c.CompanyLogoUrl,
                    CourseDescription = c.Description,
                    CourseVideoUrl = c.VideoUrl,
                    Lectures = c.Lectures
                    .Select(l => new LectureListingModel
                    {
                        Id = l.Id,
                        Title = l.Title,
                        CourseId = c.Id
                    })
                })
                .FirstOrDefaultAsync(c => c.CourseId == id);

            return courseDetails;
        }
    }
}
