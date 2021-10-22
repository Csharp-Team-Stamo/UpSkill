namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;

    public interface ICategoryService
    {
        Task<Category> GetCategory(CategoryCreateInputModel categoryInput);
        Task<Category> CreateCategory(CategoryCreateInputModel categoryInput);
    }
}
