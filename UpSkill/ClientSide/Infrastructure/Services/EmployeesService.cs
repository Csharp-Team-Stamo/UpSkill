﻿namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.WebUtilities;
    using Newtonsoft.Json;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.AddEmployeeModal;
    using JsonSerializer = System.Text.Json.JsonSerializer;

    public class EmployeesService : IEmployeesService
    {
        private readonly HttpClient httpClient;

        public EmployeesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PagingResponse<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId, EmployeesParameters parameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["companyId"] = companyId,
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("/Employees/GetCollectionByCompanyId", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<AddEmployeeFormModel>>(content);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<AddEmployeeFormModel>
            {
                Items = JsonConvert.DeserializeObject<List<AddEmployeeFormModel>>(content),
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };

            return pagingResponse;
        }

        public async Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection)
        {
            return await httpClient.PostAsJsonAsync("/Employees/PostCollection", employeesCollection);
        }
    }
}
