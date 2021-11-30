using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using UpSkill.ClientSide.Infrastructure.Services.Contracts;
using UpSkill.Infrastructure.Common.Pagination;
using UpSkill.Infrastructure.Models.Dashboard;

namespace UpSkill.ClientSide.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient httpClient;

        public DashboardService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PagingResponse<CoachDashboardStatItemModel>> GetOwnerCoachesStatsAsync(string ownerId, string month, TableEntityParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["ownerId"] = ownerId,
                ["month"] = month,
                ["pageNumber"] = parameters.PageNumber.ToString()
            };

            var response = await httpClient
                .GetAsync(QueryHelpers.AddQueryString("/Dashboard/GetOwnerCoachesStatistics", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<CoachDashboardStatItemModel>
            {
                Items = JsonConvert.DeserializeObject<List<CoachDashboardStatItemModel>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<CourseDashboardStatItemModel>> GetOwnerCoursesStatsAsync(string ownerId, string month, TableEntityParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["ownerId"] = ownerId,
                ["month"] = month,
                ["pageNumber"] = parameters.PageNumber.ToString()
            };

            var response = await httpClient
                .GetAsync(QueryHelpers.AddQueryString("/Dashboard/GetOwnerCoursesStatistics", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<CourseDashboardStatItemModel>
            {
                Items = JsonConvert.DeserializeObject<List<CourseDashboardStatItemModel>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };

            return pagingResponse;
        }
    }
}
