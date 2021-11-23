namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Course;

    public interface ICoursesService
    {
        Task<CourseDescriptionModel> GetByIdAsync(int courseId);

        CoursesListingCatalogModel GetAllByOwnerId(string ownerId);

        CoursesListingCatalogModel GetAll(string ownerId);

        Task AddCourseInOwnerCoursesCollectionAsync(int courseId, string ownerId);

        Task RemoveCourseFromOwnerCoursesCollectionAsync(int courseId, string ownerId);
    }
}
