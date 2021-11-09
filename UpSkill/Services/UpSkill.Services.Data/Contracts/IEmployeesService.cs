namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.AddEmployeeModal;

    public interface IEmployeesService
    {
        ICollection<AddEmployeeFormModel> GetByCompanyId(string companyId);

        Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees);

        string GetOwnerById(string userId);
    }
}
