namespace UpSkill.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;

    public interface IAccountsService
    {
        public Task<Company> GetCompany(string companyName);

        public bool EmailIsUnavailable(string email);
    }
}
