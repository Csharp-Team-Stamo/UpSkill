namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Services.Data.Contracts;

    public class AdminCourseService : IAdminCourseService
    {
        private readonly IRepository<Course> courseRepo;

        public AdminCourseService(IRepository<Course> courseRepo)
        {
            this.courseRepo = courseRepo;
        }

        public Task<IEnumerable<AdminCourseListingServiceModel>> All()
        {
            throw new NotImplementedException();
        }

        public async Task<int?> Create(CourseCreateInputModel input)
        {
            // TODO logic to retrieve/create the category
            // TODO logic to retrieve/create the Coach

            var course = new Course
            {
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

        public Task<int?> Edit(CourseEditInputModel input)
        {
            throw new NotImplementedException();
        }
    }
}
