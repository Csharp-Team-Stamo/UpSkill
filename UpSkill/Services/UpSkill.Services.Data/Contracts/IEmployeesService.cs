namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.AddEmployeeModal;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Services.Data.Paging;

    public interface IEmployeesService
    {

       PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, EmployeesParameters parameters);

       Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees);

       string GetOwnerById(string userId);
    }
}
