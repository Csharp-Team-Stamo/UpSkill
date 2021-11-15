namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        ICollection<string> GetAllNames();
    }
}
