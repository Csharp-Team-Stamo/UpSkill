namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;

    public partial class AdminCoach
    {
        private CoachCreateInputModel coachInput = new();

        public IEnumerable<CategoryCreateInputModel> CategoriesInDb { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            this.CategoriesInDb = await this.Client.GetFromJsonAsync<IEnumerable<CategoryCreateInputModel>>("/admin/category/all");
            // return base.OnParametersSetAsync();
        }

        public async Task Create()
        {
            var response = await this.Client.PostAsJsonAsync<CoachCreateInputModel>("/admin/coach/create", coachInput);

            if(response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/allCoaches");
            }
        }
    }
}
