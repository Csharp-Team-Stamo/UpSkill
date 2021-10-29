namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.AddEmployeeModal;

    public interface IEmployeesService
    {
        Task<ICollection<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId);

        Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection);
    }
}
