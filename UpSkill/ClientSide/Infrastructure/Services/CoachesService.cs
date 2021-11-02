namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts;
    using UpSkill.Infrastructure.Models.Coaches;

    public class CoachesService : ICoachesService
    {
        private readonly HttpClient httpClient;

        public CoachesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CoachesListingCatalogModel> GetAllAsync(string userId)
        {
           return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAll?userId={userId}");
        }

        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string userId)
        {
            return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAllByOwnerId?userId={userId}");
        }

        public async Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId)
        {
            await httpClient.PostAsJsonAsync($"/Coaches/AddCoachInOwnerCoachesCollectionAsync?coachId={coachId}&ownerId={ownerId}", string.Empty);
        }

        public async Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string userId)
        {
            await httpClient.DeleteAsync($"/Coaches/RemoveCoachFromOwnerCoachCollectionAsync?coachId={coachId}&userId={userId}");
        }
    }
}
