﻿namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Services.Data.Contracts;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly IRepository<Course> courseRepo;
        private readonly IAdminCategoryService categoryService;

        public AdminCourseService(
            IRepository<Course> courseRepo,
            IAdminCategoryService categoryService)
        {
            this.courseRepo = courseRepo;
            this.categoryService = categoryService;
        }

        public async Task<IEnumerable<AdminCourseListingServiceModel>> All()
        {
            var allCourses = await this.courseRepo.All()
                      .Select(c => new AdminCourseListingServiceModel
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Description = c.Description,
                          IsDeleted = c.IsDeleted
                      }).ToListAsync();

            return allCourses;
        }

        public async Task<int?> Create(CourseCreateInputModel input)
        {
            var category = await this.GetCategory(input.CategoryId);

            var course = new Course
            {
                Category = category,
                CategoryId = category.Id,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                AuthorFullName = input.AuthorFullName,
                AuthorCompany = input.AuthorCompany,
                Price = input.Price,
                VideoUrl = input.VideoUrl
            };

            await this.courseRepo.AddAsync(course);
            var createResult = await this.courseRepo.SaveChangesAsync();

            return createResult <= 0 ?
                null :
                course.Id;
        }

        public async Task<int?> SetDelete(int id)
        {
            var courseToDelete = this.courseRepo
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (courseToDelete == null)
            {
                return null;
            }

            courseToDelete.IsDeleted = !courseToDelete.IsDeleted;

            this.courseRepo.Update(courseToDelete);
            var deleteResult = await this.courseRepo.SaveChangesAsync();

            return deleteResult <= 0 ?
                null :
                deleteResult;
        }

        public async Task<int?> Edit(CourseEditInputModel input)
        {
            var courseToEdit = this.courseRepo
                .All()
                .FirstOrDefault(c => c.Id == input.Id);

            if(courseToEdit == null)
            {
                return null;
            }

            courseToEdit.Name = input.Name;
            courseToEdit.Description = input.Description;
            courseToEdit.ImageUrl = input.ImageUrl;
            courseToEdit.Price = input.Price;
            courseToEdit.AuthorFullName = input.AuthorFullName;
            courseToEdit.AuthorCompany = input.AuthorCompany;
            courseToEdit.VideoUrl = input.VideoUrl;

            if(courseToEdit.CategoryId != input.CategoryId)
            {
                courseToEdit.Category = await this.GetCategory(input.CategoryId);
                courseToEdit.CategoryId = input.CategoryId;
            }

            this.courseRepo.Update(courseToEdit);
            var editResult = await this.courseRepo.SaveChangesAsync();

            return editResult <= 0 ?
                null :
                courseToEdit.Id;
        }

        public async Task<Course> GetCourse(int id)
            => await this.courseRepo
            .All()
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<CourseDetailsServiceModel> GetCourseDetails(int id)
        {
            var course = await this.courseRepo
                                   .All()
                                   .Select(c => new CourseDetailsServiceModel
                                   {
                                       Id = c.Id,
                                       Name = c.Name,
                                       Description = c.Description,
                                       AuthorFullName = c.AuthorFullName,
                                       AuthorCompany = c.AuthorCompany,
                                       Category = new CategoryDetailsServiceModel
                                       {
                                           Id = c.Category.Id,
                                           Name = c.Category.Name
                                       },
                                       Price = c.Price,
                                       VideoUrl = c.VideoUrl
                                   })
                                   .FirstOrDefaultAsync(c => c.Id == id);

            return course;
        }

        private async Task<Category> GetCategory(int id)
            => await this.categoryService.GetCategory(id);
    }
}
