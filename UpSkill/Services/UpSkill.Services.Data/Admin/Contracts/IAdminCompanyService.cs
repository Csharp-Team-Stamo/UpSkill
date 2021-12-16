namespace UpSkill.Services.Data.Admin.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Company;

    public interface IAdminCompanyService
    {
        Task<int?> Create(CompanyCreateInputModel input);
    }
}
