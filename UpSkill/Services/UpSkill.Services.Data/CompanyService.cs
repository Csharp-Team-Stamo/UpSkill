namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public CompanyService(IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public async Task<bool> Exists(string companyName)
        {
            return await this.companyRepo
                .AllAsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == companyName) != null ? true : false;
        }

        public async Task<string> GetName(int id)
        {
            return await this.companyRepo
                .AllAsNoTracking()
                .Where(x => x.Id == id).Select(x => x.Name).FirstOrDefaultAsync();
        }
    }
}
