namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Infrastructure.Models.Language;

    public partial class AdminCourse : ComponentBase
    {
        private readonly CourseCreateInputModel courseInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb { get; set; } 
            = new List<AdminCategoryListingServiceModel>();

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
                .PostAsJsonAsync("admin/course/create", courseInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/course/all");
            }
        }
    }
}
