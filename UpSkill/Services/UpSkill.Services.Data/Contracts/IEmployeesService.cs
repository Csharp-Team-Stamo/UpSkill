namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Common.Pagination;
    using Infrastructure.Models.Employee;
    using Paging;

    public interface IEmployeesService
    {
       string GetOwnerIdByAppUserId(string userId);

       string GetEmployeeIdByAppUserId(string userId);

       PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, EmployeesParameters parameters);

       Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees);

       Task EnrollToCourseAsync(int courseId, string employeeId);

       bool IsEmployeeEnrolledForCourse(string employeeId, int courseId);
    }
}
