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

       Task<ICollection<string>> SaveEmployeesCollectionAsync(
           ICollection<AddEmployeeFormModel> employees);

       string GetOwnerById(string userId);

        Task<EmployeeCourseDetailsModel> GetCourseById(int id);
    }
}
