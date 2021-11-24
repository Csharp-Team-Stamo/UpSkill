namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Models.Category;
    using Infrastructure.Models.Course;
    using Contracts;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly IDeletableEntityRepository<Course> courseRepo;
        private readonly IAdminCategoryService categoryService;
        private readonly IAdminLanguageService languageService;
        private readonly IDeletableEntityRepository<CourseOwner> courseOwnersRepo;
        private readonly IDeletableEntityRepository<CourseVote> courseVotesRepo;
        private readonly IDeletableEntityRepository<Grade> gradesRepo;
        private readonly IDeletableEntityRepository<EmployeeCourse> employeeCourseRepo;

        public AdminCourseService(
            IDeletableEntityRepository<Course> courseRepo,
            IAdminCategoryService categoryService,
            IAdminLanguageService languageService,
            IDeletableEntityRepository<CourseOwner> courseOwnersRepo,
            IDeletableEntityRepository<CourseVote> courseVotesRepo,
            IDeletableEntityRepository<Grade> gradesRepo,
            IDeletableEntityRepository<EmployeeCourse> employeeCourseRepo)
        {
            this.courseRepo = courseRepo;
            this.categoryService = categoryService;
            this.languageService = languageService;
            this.courseOwnersRepo = courseOwnersRepo;
            this.courseVotesRepo = courseVotesRepo;
            this.gradesRepo = gradesRepo;
            this.employeeCourseRepo = employeeCourseRepo;
        }

        public async Task<IEnumerable<AdminCourseListingServiceModel>> All()
        {
            var allCourses = await this.courseRepo.All()
                      .Select(c => new AdminCourseListingServiceModel
                      {
                          Id = c.Id,
                          Name = c.Name,
                          ImageUrl = c.ImageUrl,
                          CategoryName = c.Category.Name,
                          Price = c.Price,
                          AuthorFullName = c.AuthorFullName,
                          AuthorCompanyLogo = c.CompanyLogoUrl,
                          IsDeleted = c.IsDeleted
                      }).ToListAsync();

            return allCourses;
        }

        public async Task<int?> Create(CourseCreateInputModel input)
        {
            var category = await this.GetCategory(input.CategoryId);
            var language = await this.GetLanguage(input.LanguageId);

            var course = new Course
            {
                Category = category,
                CategoryId = category.Id,
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl,
                AuthorFullName = input.AuthorFullName,
                CompanyLogoUrl = input.CompanyLogoUrl,
                CompanyName = input.CompanyName,
                Price = input.Price,
                VideoUrl = input.VideoUrl,
                Language = language,
                LanguageId = language.Id
            };

            await this.courseRepo.AddAsync(course);
            var createResult = await this.courseRepo.SaveChangesAsync();

            return createResult <= 0 ?
                null :
                course.Id;
        }

        public async Task<int?> SetDeleted(int id)
        {
            var courseToDelete = this.courseRepo
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (courseToDelete == null)
            {
                return null;
            }

            await DeleteInCourseOwnersTable(courseToDelete.Id);
            await DeleteInCourseVotesTable(courseToDelete.Id);
            await DeleteCourseInGradesTable(courseToDelete.Id);
            await DeleteInEmployeeCoursesTable(courseToDelete.Id);

            courseToDelete.IsDeleted = !courseToDelete.IsDeleted;

            this.courseRepo.Update(courseToDelete);
            var deleteResult = await this.courseRepo.SaveChangesAsync();

            return deleteResult <= 0 ?
                null :
                deleteResult;
        }

        private async Task DeleteInEmployeeCoursesTable(int courseId)
        {
            var employeeCourses = await this.employeeCourseRepo
                .All()
                .Where(ec => ec.Id == courseId)
                .ToListAsync();

            if(employeeCourses.Any() == false)
            {
                return;
            }

            employeeCourses.ForEach(ec => this.employeeCourseRepo.Delete(ec));

            await this.employeeCourseRepo.SaveChangesAsync();
        }

        private async Task DeleteCourseInGradesTable(int courseId)
        {
            var courseGrades = await this.gradesRepo
                .All()
                .Where(g => g.Course.Id == courseId)
                .ToListAsync();

            if(courseGrades.Any() == false)
            {
                return;
            }

            courseGrades.ForEach(cg => this.gradesRepo.Delete(cg));

            await this.gradesRepo.SaveChangesAsync();
        }

        private async Task DeleteInCourseVotesTable(int courseId)
        {
            var courseVotes = await this.courseVotesRepo
                .All()
                .Where(cv => cv.Course.Id == courseId)
                .ToListAsync();

            if(courseVotes.Any() == false)
            {
                return;
            }

            courseVotes.ForEach(cv => this.courseVotesRepo.Delete(cv));

            await this.courseVotesRepo.SaveChangesAsync();
        }

        private async Task DeleteInCourseOwnersTable(int courseId)
        {
            var courseOwners = await this.courseOwnersRepo
                .All()
                .Where(c => c.Id == courseId)
                .ToListAsync();

            if(courseOwners.Any() == false)
            {
                return;
            }

            courseOwners.ForEach(co => this.courseOwnersRepo.Delete(co));

            await this.courseOwnersRepo.SaveChangesAsync();
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
            courseToEdit.CompanyLogoUrl = input.CompanyLogoUrl;
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
                                       ImageUrl = c.ImageUrl,
                                       Description = c.Description,
                                       AuthorFullName = c.AuthorFullName,
                                       CompanyLogoUrl = c.CompanyLogoUrl,
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

        private async Task<Language> GetLanguage(int id)
            => await this.languageService.GetLanguage(id);
    }
}
