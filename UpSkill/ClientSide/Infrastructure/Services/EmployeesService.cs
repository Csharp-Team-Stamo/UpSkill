namespace UpSkill.ClientSide.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.AddEmployeeModal;

    public class EmployeesService : IEmployeesService
    {
        private readonly HttpClient httpClient;

        public EmployeesService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ICollection<AddEmployeeFormModel>> GetCollectionFromDbByCompanyIdAsync(string companyId)
        {
            return await httpClient.GetFromJsonAsync<ICollection<AddEmployeeFormModel>>($"/Employees/GetCollectionByCompanyId/{companyId}");
        }

        public async Task<HttpResponseMessage> SaveCollectionInDbAsync(ICollection<AddEmployeeFormModel> employeesCollection)
        {
            return await httpClient.PostAsJsonAsync("/Employees/PostCollection", employeesCollection);
        }
    }
}
