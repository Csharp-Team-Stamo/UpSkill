namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Infrastructure.Models.Course;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.Dashboard;
    using UpSkill.Services.Data.Paging;

    public interface ICoursesService
    {
        Task AddCourseInOwnerCoursesCollectionAsync(int courseId, string ownerId);

        Task<CourseDescriptionModel> GetByIdAsync(int courseId);

        CoursesListingCatalogModel GetAllByOwnerId(string ownerId);

        Task<List<CourseInListCatalogModel>> GetAllEnrolledByEmployeeIdAsync(string employeeId);

        CoursesListingCatalogModel GetAll(string ownerId);

        Task RemoveCourseFromOwnerCoursesCollectionAsync(int courseId, string ownerId);

        PagedList<CourseDashboardStatItemModel> GetDashboardCourses(string ownerId, int month, TableEntityParameters parameters);
    }
}
