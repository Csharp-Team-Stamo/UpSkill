namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Services.Data.Contracts;

    public class AdminCourseService : IAdminCourseService
    {
        public Task<IEnumerable<AdminCourseListingServiceModel>> All()
        {
            throw new NotImplementedException();
        }

        public Task<int?> Create(CourseCreateInputModel input)
        {
            var course = new Course
            {

            };

            return null;
        }

        public Task<int?> Edit(CourseEditInputModel input)
        {
            throw new NotImplementedException();
        }
    }
}
