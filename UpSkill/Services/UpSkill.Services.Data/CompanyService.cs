namespace UpSkill.Services.Data
{
    using System.Linq;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Contracts;

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
    }
}
