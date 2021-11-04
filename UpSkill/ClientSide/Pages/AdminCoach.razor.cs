namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;

    public partial class AdminCoach : ComponentBase
    {
        private readonly CoachCreateInputModel coachInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb =
            new List<AdminCategoryListingServiceModel>();

        //[Inject]
        //public HttpClient Client { get; set; }

        //[Inject]
        //public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.CategoriesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>
                ("/admin/category/all");
        }

        public async Task Create()
        {
            var response = await this.Client.PostAsJsonAsync<CoachCreateInputModel>("/admin/coach/create", coachInput);

            if(response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/coach/all");
            }
        }
    }
}
