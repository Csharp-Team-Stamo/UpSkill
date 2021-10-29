using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Course;

namespace UpSkill.Services.Data.Contracts
{
    public interface IAdminCourseService
    {
        Task<int?> Create(CourseCreateInputModel input);
        Task<int?> Edit(CourseEditInputModel input);
        Task<Course> GetCourse(int id);

        Task<CourseDetailsServiceModel> GetCourseDetails(int id);
        Task<IEnumerable<AdminCourseListingServiceModel>> All();
        Task<int?> SetDelete(int id);
    }
}
