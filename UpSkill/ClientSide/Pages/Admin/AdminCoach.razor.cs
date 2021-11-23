namespace UpSkill.ClientSide.Pages.Admin
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Infrastructure.Models.Language;


    public partial class AdminCoach : ComponentBase
    {
        private readonly CoachCreateInputModel coachInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb =
            new List<AdminCategoryListingServiceModel>();

        public IEnumerable<LanguageListingServiceModel> LanguagesInDb { get; set; } =
            new List<LanguageListingServiceModel>();

        protected override async Task OnInitializedAsync()
        {
            this.CategoriesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>
                ("/admin/category/all");

            this.LanguagesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<LanguageListingServiceModel>>
                ("/admin/language/all");
        }

        public async Task Create()
        {
            var response = await this.Client
                .PostAsJsonAsync<CoachCreateInputModel>("/admin/coach/create", coachInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/coach/all");
            }
        }
    }
}
