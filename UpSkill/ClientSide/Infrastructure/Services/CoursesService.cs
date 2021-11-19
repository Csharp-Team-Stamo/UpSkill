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
    }
}
