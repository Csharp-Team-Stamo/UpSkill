namespace UpSkill.ClientSide.Infrastructure.Services.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Course;

    public interface ICoursesService
    {
        Task<CoursesListingCatalogModel> GetAllAsync(string ownerId);
    }
}
