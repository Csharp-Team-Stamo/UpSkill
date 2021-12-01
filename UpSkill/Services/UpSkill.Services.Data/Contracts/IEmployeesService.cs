﻿namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Common.Pagination;
    using Infrastructure.Models.Employee;
    using Paging;
    using UpSkill.Infrastructure.Models.Course;

    public interface IEmployeesService
    {
        Task<EmployeeAchievementsModel> GetEmployeeAchievementsInfoAsync(string employeeId);

        string GetOwnerIdByAppUserId(string userId);

        string GetEmployeeIdByAppUserId(string userId);

        PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, TableEntityParameters parameters);

        Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees);

        Task EnrollToCourseAsync(int courseId, string employeeId);

        bool IsEmployeeEnrolledForCourse(string employeeId, int courseId);

        Task<CourseWatchDetailsModel> GetCourseById(int id);
    }
}
