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

        public async Task<CoachesListingCatalogModel> GetAllAsync(string ownerId)
        {
           return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAll?ownerId={ownerId}");
        }

        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId)
        {
            return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAllByOwnerId?ownerId={ownerId}");
        }

        public async Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId)
        {
            await httpClient.PostAsJsonAsync($"/Coaches/AddCoachInOwnerCoachesCollectionAsync?coachId={coachId}&ownerId={ownerId}", string.Empty);
        }

        public async Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId)
        {
            await httpClient.DeleteAsync($"/Coaches/RemoveCoachFromOwnerCoachCollectionAsync?coachId={coachId}&ownerId={ownerId}");
        }
    }
}
