namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;

    public interface IAdminCategoryService
    {
        Task<Category> GetCategory(int id);
        Task<CategoryDetailsServiceModel> GetCategoryDetails(int id);
        Task<int?> CreateCategory(CategoryCreateInputModel categoryInput);
        Task<IEnumerable<AdminCategoryListingServiceModel>> GetAll();
        Task<int?> Edit(CategoryEditInputModel editInput);
        Task<int?> Delete(int id);
    }
}
