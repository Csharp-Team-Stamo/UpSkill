namespace UpSkill.ClientSide.Infrastructure.Services
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
    using UpSkill.ClientSide.Infrastructure.Features;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.AddEmployeeModal;

    public class EmployeesService : IEmployeesService
    {
        private readonly HttpClient httpClient;

        public EmployeesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PagingResponse<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId, EmployeesParameters parameters)
        {


            //return await httpClient
            //    .GetFromJsonAsync<ICollection<AddEmployeeFormModel>>
            //    ($"/Employees/GetCollectionByCompanyId/{companyId}");

            var queryStringParam = new Dictionary<string, string>
            {
                ["companyId"] = companyId,
                ["pageNumber"] = parameters.PageNumber.ToString()
            };
            var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("/Employees/GetCollectionByCompanyId", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<AddEmployeeFormModel>
            {
                Items = JsonSerializer.Deserialize<List<AddEmployeeFormModel>>(content, new JsonSerializerOptions { WriteIndented = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { WriteIndented = true })
            };

            foreach (var item in pagingResponse.Items)
            {
                Console.WriteLine($"{item.FullName} - {item.Email}");
            }         
            Console.WriteLine(pagingResponse.MetaData);

            return pagingResponse;
        }

        public async Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection)
        {
            return await httpClient.PostAsJsonAsync("/Employees/PostCollection", employeesCollection);
        }
    }
}
