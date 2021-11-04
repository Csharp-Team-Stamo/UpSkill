namespace UpSkill.ClientSide.Pages
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;

    public partial class AdminCategory
    {
        private readonly CategoryCreateInputModel categoryInput = new();

        public async Task Create()
        {
            var response = await this.Client
                .PostAsJsonAsync<CategoryCreateInputModel>
                ("/admin/category/create", categoryInput);

            if(response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/category/all");
            }
        }
    }
}
