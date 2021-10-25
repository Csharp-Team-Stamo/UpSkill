namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;

    public interface ICategoryService
    {
        Task<Category> GetCategory(int id);
        // Task<Category> GetCategory(CategoryCreateInputModel categoryInput);
        Task<Category> CreateCategory(CategoryCreateInputModel categoryInput);
        Task<IEnumerable<CategoryCreateInputModel>> GetAll();
    }
}
