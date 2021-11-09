namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ILanguagesService
    {
        ICollection<string> GetAll();
    }
}
