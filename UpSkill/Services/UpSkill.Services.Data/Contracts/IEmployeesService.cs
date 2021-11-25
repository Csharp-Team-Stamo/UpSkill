namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Common.Pagination;
    using Infrastructure.Models.Employee;
    using Paging;

    public interface IEmployeesService
    {

       PagedList<AddEmployeeFormModel> GetByCompanyId(
           string companyId, EmployeesParameters parameters);

        bool IsEmployeeEnrolledForCourse(string employeeId, int courseId);

        string GetEmployeeIdByAppUserId(string userId);

        Task<ICollection<string>> SaveEmployeesCollectionAsync(
           ICollection<AddEmployeeFormModel> employees);

       string GetOwnerById(string userId);

        Task<EmployeeCourseDetailsModel> GetCourseById(int id);
    }
}
