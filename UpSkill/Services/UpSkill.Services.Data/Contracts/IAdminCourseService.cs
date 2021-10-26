using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Course;

namespace UpSkill.Services.Data.Contracts
{
    public interface IAdminCourseService
    {
        Task<int?> Create(CourseCreateInputModel input);
        Task<int?> Edit(CourseEditInputModel input);

        Task<CourseDetailsServiceModel> GetCourse(int id);
        Task<IEnumerable<AdminCourseListingServiceModel>> All();
        Task<int?> Delete(int id);
    }
}
