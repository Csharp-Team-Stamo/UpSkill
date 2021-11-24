﻿namespace UpSkill.Services.Data
{
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
    using Paging;

    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAccountsService accountsService;
        private readonly IOwnerService ownerService;
        private readonly IDeletableEntityRepository<EmployeeCourse> employeeCourseRepository;

        public EmployeesService(IDeletableEntityRepository<Employee> employeeRepository, UserManager<ApplicationUser> userManager, IAccountsService accountsService, IOwnerService ownerService, IDeletableEntityRepository<EmployeeCourse> employeeCourseRepository)
        {
            this.employeeRepository = employeeRepository;
            this.userManager = userManager;
            this.accountsService = accountsService;
            this.ownerService = ownerService;
            this.employeeCourseRepository = employeeCourseRepository;
        }

        public string GetEmployeeIdByAppUserId(string userId)
        {
            return this.employeeRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).Id;
        }

        public PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, EmployeesParameters parameters)
        {
            var employees = employeeRepository.All().Where(x => x.User.CompanyId == int.Parse(companyId)).Select(x =>
               new AddEmployeeFormModel { FullName = x.User.FullName, Email = x.User.Email, }).ToList();

            return PagedList<AddEmployeeFormModel>.ToPagedList(employees, parameters.PageNumber, parameters.PageSize);
        }

        public string GetOwnerIdByAppUserId(string userId)
        {
            return this.employeeRepository.AllAsNoTracking().FirstOrDefault(x => x.UserId == userId).OwnerId;
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
                        OwnerId = ownerService.GetId(employeeModel.UserId),
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
    }
}
