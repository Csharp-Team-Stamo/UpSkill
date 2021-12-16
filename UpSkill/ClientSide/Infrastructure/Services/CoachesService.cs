namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json;
    using UpSkill.Infrastructure.Models.Coach;

    public class CoachesService : ICoachesService
    {
        private readonly HttpClient httpClient;

        public CoachesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CoachesListingCatalogModel> GetAllAsync(string ownerId)
        {
           return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAllAsync?ownerId={ownerId}");
        }

        public async Task<CoachesListingCatalogModel> GetAllByEmployeeIdAsync(string ownerId, string userId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["ownerId"] = ownerId,
                ["userId"] = userId
            };

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("/Coaches/GetAllByEmployeeIdAsync", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CoachesListingCatalogModel>(content);

            return result;
        }

        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["ownerId"] = ownerId,
            };

            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("/Coaches/GetAllByOwnerIdAsync", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CoachesListingCatalogModel>(content);

            return result;
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
