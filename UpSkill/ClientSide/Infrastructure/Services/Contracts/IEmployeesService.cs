namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.AddEmployeeModal;

    public interface IEmployeesService
    {
        //Task<ICollection<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId);

        Task<PagingResponse<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId, EmployeesParameters parameters);

        Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection);
    }
}
