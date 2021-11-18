namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Employee;
    using UpSkill.Infrastructure.Common.Pagination;

    public interface IEmployeesService
    {
        //Task<ICollection<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId);

        Task<PagingResponse<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId, EmployeesParameters parameters);

        Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection);
    }
}
