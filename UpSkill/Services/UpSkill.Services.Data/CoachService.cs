﻿namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Services.Data.Contracts;

    public class CoachService : ICoachService
    {
        private readonly ICategoryService categoryService;
        private readonly IDeletableEntityRepository<Coach> coachRepo;

        public CoachService(
            ICategoryService categoryService,
            IDeletableEntityRepository<Coach> coachRepo)
        {
            this.categoryService = categoryService;
            this.coachRepo = coachRepo;
        }

        public async Task<Coach> Create(CoachCreateInputModel coachInput)
        {
            var category = await this.categoryService.GetCategory(coachInput.CategoryId);

            //if(category == null)
            //{
            //    category = await this.categoryService.CreateCategory(coachInput.Category);
            //}

            var coach = new Coach
            {
                Category = category,
                CategoryId = category.Id,
                Company = coachInput.Company,
                FullName = coachInput.FullName,
                PricePerSession = coachInput.PricePerSession
            };

            await this.coachRepo.AddAsync(coach);
            await this.coachRepo.SaveChangesAsync();

            return coach;
        }

        public async Task<Coach> GetCoach(string id)
        {
            var coach = await this.coachRepo.All().FirstOrDefaultAsync(c => c.Id == id);

            return coach;
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
    }
}
