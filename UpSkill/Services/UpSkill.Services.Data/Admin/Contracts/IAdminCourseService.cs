namespace UpSkill.Services.Data.Admin.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Course;

    public interface IAdminCourseService
    {
        Task<int?> Create(CourseCreateInputModel input);
        Task<int?> Edit(CourseEditInputModel input);
        Task<Course> GetCourse(int id);

        Task<CourseDetailsServiceModel> GetCourseDetails(int id);
        Task<IEnumerable<AdminCourseListingServiceModel>> All();
        Task<int?> SetDeleted(int id);
    }
}
