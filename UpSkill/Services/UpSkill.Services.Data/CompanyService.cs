namespace UpSkill.Services.Data
{
    using System.Linq;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Services.Data.Contracts;

    public class CompanyService : ICompanyService
    {
        private readonly IDeletableEntityRepository<Company> companyRepo;

        public CompanyService(IDeletableEntityRepository<Company> companyRepo)
        {
            this.companyRepo = companyRepo;
        }

        public bool Exists(string companyName) => 
            this.companyRepo
            .AllAsNoTracking()
            .FirstOrDefault(x => x.Name == companyName) != null ? true : false;

        public string GetName(int id)
        => this.companyRepo
            .AllAsNoTracking()
            .FirstOrDefault(x => x.Id == id).Name;
    }
}
