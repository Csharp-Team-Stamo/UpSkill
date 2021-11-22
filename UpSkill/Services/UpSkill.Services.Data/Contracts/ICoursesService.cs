namespace UpSkill.Services.Data.Contracts
{
    using Infrastructure.Models.Course;

    public interface ICoursesService
    {
        CoursesListingCatalogModel GetAll(string ownerId);
    }
}
