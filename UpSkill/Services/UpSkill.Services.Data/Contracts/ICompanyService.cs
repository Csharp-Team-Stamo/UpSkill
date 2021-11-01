namespace UpSkill.Services.Data.Contracts
{
    public interface ICompanyService
    {
        bool Exists(string companyName);

        string GetName(int id);
    }
}
