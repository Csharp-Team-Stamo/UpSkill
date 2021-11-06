﻿namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Services.Data.Contracts;

    public class AdminCoachService : IAdminCoachService
    {
        private readonly IAdminCategoryService categoryService;
        private readonly IDeletableEntityRepository<Coach> coachRepo;

        public AdminCoachService(
            IAdminCategoryService categoryService,
            IDeletableEntityRepository<Coach> coachRepo)
        {
            this.categoryService = categoryService;
            this.coachRepo = coachRepo;
        }

        public async Task<Coach> Create(CoachCreateInputModel coachInput)
        {
            var category = await this.categoryService.GetCategory(coachInput.CategoryId);

            var coach = new Coach
            {
                Category = category,
                CategoryId = category.Id,
                Company = coachInput.Company,
                FullName = coachInput.FullName,
                ImageUrl = coachInput.ImageUrl,
                PricePerSession = coachInput.PricePerSession
            };

            await this.coachRepo.AddAsync(coach);
            await this.coachRepo.SaveChangesAsync();

            return coach;
        }

        public async Task<CoachDetailsServiceModel> GetCoachDetails(string id)
        {
            var coachInDb = await this.GetCoach(id);

            if(coachInDb == null)
            {
                return null;
            }

            var coachDetails = new CoachDetailsServiceModel
            {
                Id = coachInDb.Id,
                FullName = coachInDb.FullName,
                ImageUrl = coachInDb.ImageUrl,
                Category = new CategoryDetailsServiceModel
                {
                    Id = coachInDb.Category.Id,
                    Name = coachInDb.Category.Name
                },
                Company = coachInDb.Company,
                PricePerSession = coachInDb.PricePerSession
            };

            return coachDetails;
        }

        public async Task<IEnumerable<AdminCoachListingServiceModel>> GetAll()
        {
            var allCoaches = await this.coachRepo
                .All()
                .Select(c => new AdminCoachListingServiceModel
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .ToListAsync();

            return allCoaches;
        }

        public async Task<int?> ExecuteEdit(CoachEditInputModel editInput)
        {
            var coachToEdit = await this.GetCoach(editInput.Id);

            if(coachToEdit == null)
            {
                return null;
            }

            coachToEdit.FullName = editInput.FullName;
            coachToEdit.ImageUrl = editInput.ImageUrl;
            coachToEdit.PricePerSession = editInput.PricePerSession;
            coachToEdit.Company = editInput.Company;
            
            if(coachToEdit.Category.Id != editInput.CategoryId)
            {
                coachToEdit.Category = await this.GetCategory(editInput.CategoryId);
                coachToEdit.CategoryId = editInput.CategoryId;
            }

            this.coachRepo.Update(coachToEdit);
            var editResult = await this.coachRepo.SaveChangesAsync();

            return editResult;
        }

        public async Task<int?> Delete(string id)
        {
            var coachToDelete = await this.GetCoach(id);

            if(coachToDelete == null)
            {
                return null;
            }

            this.coachRepo.Delete(coachToDelete);
            var deleteResult = await this.coachRepo.SaveChangesAsync();

            return deleteResult;
        }

        public async Task<CoachEditInputModel> GetCoachEditModel(string id)
        {
            var coachToEdit = await this.GetCoach(id);

            if(coachToEdit == null)
            {
                return null;
            }

            var editModel = ConvertToEditModel(coachToEdit);

            return editModel;
        }

        public async Task<Coach> GetCoach(string id)
            => await this.coachRepo
                        .All()
                        .Include(c => c.Category)
                        .FirstOrDefaultAsync(c => c.Id == id);

        private async Task<Category> GetCategory(int id)
            => await this.categoryService.GetCategory(id);

        private static CoachEditInputModel ConvertToEditModel(Coach coachInDb)
            => new()
            {
                Id = coachInDb.Id,
                Company = coachInDb.Company,
                FullName = coachInDb.FullName,
                ImageUrl = coachInDb.ImageUrl,
                PricePerSession = coachInDb.PricePerSession,
                CategoryId = coachInDb.Category.Id,
                CategoryName = coachInDb.Category.Name
            };
    }
}