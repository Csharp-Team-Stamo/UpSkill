namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILanguagesService
    {
        Task<ICollection<string>> GetAll();
    }
}
