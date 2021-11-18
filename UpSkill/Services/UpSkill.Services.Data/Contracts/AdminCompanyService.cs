using System.Threading.Tasks;
using UpSkill.Data.Common.Repositories;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Company;

namespace UpSkill.Services.Data.Contracts
{
    public class AdminCompanyService : IAdminCompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public AdminCompanyService(IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public async Task<int?> Create(CompanyCreateInputModel input)
        {
            var company = new Company
            {
                Address = input.Address,
                Email = input.Email,
                Name = input.Name,
                UIC = input.UIC
            };

            await this.companyRepo.AddAsync(company);
            var createResult = await this.companyRepo.SaveChangesAsync();

            return createResult <= 0 ?
                null :
                company.Id;
        }
    }
}
