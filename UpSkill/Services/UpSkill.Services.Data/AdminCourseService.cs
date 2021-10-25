namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Services.Data.Contracts;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly IRepository<Course> courseRepo;
        private readonly ICategoryService categoryService;
        private readonly ICoachService coachService;

        public AdminCourseService(
            IRepository<Course> courseRepo,
            ICategoryService categoryService,
            ICoachService coachService)
        {
            this.courseRepo = courseRepo;
            this.categoryService = categoryService;
            this.coachService = coachService;
        }

        public async Task<IEnumerable<AdminCourseListingServiceModel>> All()
        {
            var allCourses = await this.courseRepo.All()
                      .Select(c => new AdminCourseListingServiceModel
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Description = c.Description

                      }).ToListAsync();

            return allCourses;
        }

        public async Task<int?> Create(CourseCreateInputModel input)
        {
            var category = await this.categoryService.GetCategory(input.Category.Id);

            if(category == null)
            {
                category = await this.categoryService.CreateCategory(input.Category);
            }

            var coach = await this.coachService.GetCoach(input.Coach);

            if(coach == null)
            {
                coach = await this.coachService.Create(input.Coach);
            }

            var course = new Course
            {
                Category = category,
                CategoryId = category.Id,
                Coach = coach,
                CoachId = coach.Id,
                Name = input.Name,
                Description = input.Description,
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

        public async Task<int?> Delete(int id)
        {
            var courseToDelete = this.courseRepo
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (courseToDelete == null)
            {
                return null;
            }

            this.courseRepo.Delete(courseToDelete);
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
            courseToEdit.Price = input.Price;
            courseToEdit.AuthorFullName = input.AuthorFullName;
            courseToEdit.AuthorCompany = input.AuthorCompany;
            courseToEdit.VideoUrl = input.VideoUrl;

            // TODO complete with category and coach

            this.courseRepo.Update(courseToEdit);
            var editResult = await this.courseRepo.SaveChangesAsync();

            return editResult <= 0 ?
                null :
                courseToEdit.Id;
        }
    }
}
