namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json;
    using UpSkill.Infrastructure.Common.Pagination.Coaches;
    using UpSkill.Infrastructure.Models.Coaches;

    public class CoachesService : ICoachesService
    {
        private readonly HttpClient httpClient;

        public CoachesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CoachesListingCatalogModel> GetAllAsync(string ownerId, CoachesParameters parameters)
        {
            //return await httpClient.GetFromJsonAsync<CoachesListingCatalogModel>($"/coaches/GetAll?ownerId={ownerId}");

            var queryStringParam = new Dictionary<string, string>
            {
                ["pageSize"] = parameters.PageSize.ToString(),
                ["startIndex"] = parameters.StartIndex.ToString(),
                ["оwnerId"] = ownerId
            };

            var response =
                await httpClient.GetAsync(QueryHelpers.AddQueryString("/coaches/GetAll", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            var virtualizeResponse = JsonConvert.DeserializeObject<CoachesListingCatalogModel>(content);
            System.Console.WriteLine($"{virtualizeResponse.Coaches.Items}");
            foreach (var item in virtualizeResponse.Coaches.Items)
            {
                System.Console.WriteLine($"{item.FullName}");

            }
            return virtualizeResponse;
        }

        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId, CoachesParameters parameters)
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
