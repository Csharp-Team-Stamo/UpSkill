namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.AddEmployeeModal;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Services.Data.Paging;
    //using UpSkill.Services.Data.RequestFeatures;

    public interface IEmployeesService
    {
        //Task<PagedList<Product>> GetProducts(ProductParameters productParameters);

       PagedList<AddEmployeeFormModel> GetByCompanyId(string companyId, EmployeesParameters parameters);


        //ICollection<AddEmployeeFormModel> GetByCompanyId(string companyId);

        Task<ICollection<string>> SaveEmployeesCollectionAsync(ICollection<AddEmployeeFormModel> employees);

        string GetOwnerById(string userId);
    }
}
