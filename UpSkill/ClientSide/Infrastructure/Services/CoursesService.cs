namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts;
    using UpSkill.Infrastructure.Models.Course;

    public class CoursesService : ICoursesService
    {
        private readonly HttpClient httpClient;

        public CoursesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CoursesListingCatalogModel> GetAllAsync(string ownerId)
        {
            return await httpClient.GetFromJsonAsync<CoursesListingCatalogModel>($"/Courses/GetAll?ownerId={ownerId}");
        }

        public async Task AddCourseInOwnerCoursesCollectionAsync(int courseId, string ownerId)
        {
            await httpClient.PostAsJsonAsync($"/Courses/AddCourseInOwnerCoursesCollectionAsync?courseId={courseId}&ownerId={ownerId}", string.Empty);
        }

        public async Task RemoveCourseFromOwnerCoursesCollectionAsync(int courseId, string ownerId)
        {
            await httpClient.DeleteAsync($"/Courses/RemoveCourseFromOwnerCoursesCollectionAsync?courseId={courseId}&ownerId={ownerId}");
        }
    }
}
