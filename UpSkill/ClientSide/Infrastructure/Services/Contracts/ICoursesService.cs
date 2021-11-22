namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Course;

    public interface ICoursesService
    {
        Task<CoursesListingCatalogModel> GetAllAsync(string ownerId);

        Task<CoursesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId);

        Task AddCourseInOwnerCoursesCollectionAsync(int courseId, string ownerId);

        Task RemoveCourseFromOwnerCoursesCollectionAsync(int courseId, string ownerId);
    }
}
