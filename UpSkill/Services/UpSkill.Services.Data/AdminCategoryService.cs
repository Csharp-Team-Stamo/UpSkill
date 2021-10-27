namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Services.Data.Contracts;

    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public AdminCategoryService(IDeletableEntityRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<int?> CreateCategory(CategoryCreateInputModel categoryInput)
        {
            var category = new Category
            {
                Name = categoryInput.Name
            };

            await this.categoryRepo.AddAsync(category);
            var createResult = await this.categoryRepo.SaveChangesAsync();

            return createResult;
        }

        public async Task<IEnumerable<AdminCategoryListingServiceModel>> GetAll()
        {
            var allCategories = await this.categoryRepo
                .All()
                .Select(c => new AdminCategoryListingServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return allCategories;
        }

        public async Task<Category> GetCategory(int id)
        {
            var category = await this.categoryRepo
                                     .All()
                                     .FirstOrDefaultAsync(c => c.Id == id);

            return category;
        }

        public async Task<CategoryDetailsServiceModel> GetCategoryDetails(int id)
        {
            var categoryDetails = await this.categoryRepo
                .AllAsNoTracking()
                .Select(c => new CategoryDetailsServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync(category => category.Id == id);

            return categoryDetails;
        }

        public async Task<int?> Edit(CategoryEditInputModel editInput)
        {
            var categoryToEdit = await this.GetCategory(editInput.Id);

            if(categoryToEdit == null)
            {
                return null;
            }

            categoryToEdit.Name = editInput.Name;
            categoryToEdit.ModifiedOn = DateTime.UtcNow;

            this.categoryRepo.Update(categoryToEdit);
            var editResult = await this.categoryRepo.SaveChangesAsync();

            return editResult;
        }

        public async Task<int?> Delete(int id)
        {
            var categoryToDelete = await this.GetCategory(id);

            if(categoryToDelete == null)
            {
                return null;
            }

            this.categoryRepo.Delete(categoryToDelete);
            var deleteResult = await this.categoryRepo.SaveChangesAsync();

            return deleteResult;
        }
    }
}
