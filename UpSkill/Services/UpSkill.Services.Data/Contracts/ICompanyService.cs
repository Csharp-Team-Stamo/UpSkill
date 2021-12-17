namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface ICompanyService
    {
        Task<bool> Exists(string companyName);

        Task<string> GetName(int id);
    }
}
