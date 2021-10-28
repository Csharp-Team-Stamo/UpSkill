namespace UpSkill.ClientSide.Pages
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Infrastructure.Models.Course;

    public partial class AdminCourse : ComponentBase
    {
        private readonly CourseCreateInputModel courseInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb { get; set; } 
            = new List<AdminCategoryListingServiceModel>();

        //public IEnumerable<AdminCoachListingServiceModel> CoachesInDb { get; set; } 
        //    = new List<AdminCoachListingServiceModel>();

        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.CategoriesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>("/admin/category/all");
        }

        public async Task Create()
        {
            var response = await this.Client.PostAsJsonAsync("admin/course/create", courseInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/course/all");
            }
        }
    }
}
