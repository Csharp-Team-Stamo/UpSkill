using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UpSkill.Data.Common.Repositories;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Category;
using UpSkill.Services.Data.Contracts;

namespace UpSkill.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepo;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<Category> CreateCategory(CategoryCreateInputModel categoryInput)
        {
            var category = new Category
            {
                Name = categoryInput.Name
            };

            await this.categoryRepo.AddAsync(category);
            await this.categoryRepo.SaveChangesAsync();

            return category;
        }

        public async Task<IEnumerable<CategoryCreateInputModel>> GetAll()
        {
            var allCategories = await this.categoryRepo
                .All()
                .Select(c => new CategoryCreateInputModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return allCategories;
        }

        public async Task<Category> GetCategory(CategoryCreateInputModel categoryInput)
        {
            var category = await this.categoryRepo
                                     .All()
                                     .FirstOrDefaultAsync(c => c.Id == categoryInput.Id);

            if (category == null)
            {
                category = await this.CreateCategory(categoryInput);
            }

            return category;
        }
    }
}
