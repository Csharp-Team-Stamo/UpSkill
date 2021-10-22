namespace UpSkill.ClientSide.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Infrastructure.Models.Coach;
    using Microsoft.AspNetCore.Components;
    using System.Net.Http;
    using System.Net.Http.Json;
    using UpSkill.Infrastructure.Models.Category;

    public partial class Admin
    {
        private readonly CourseCreateInputModel courseInput = new();

        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public IEnumerable<CategoryCreateInputModel> CategoriesInDb { get; set; }

        public IEnumerable<CoachCreateInputModel> CoachesInDb { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.CategoriesInDb = await this.Client.GetFromJsonAsync<IEnumerable<CategoryCreateInputModel>>("admin/category/all");
            this.CoachesInDb = await this.Client.GetFromJsonAsync<IEnumerable<CoachCreateInputModel>>("admin/coach/all");

            // return base.OnInitializedAsync(); 
        }

        public async Task Create()
        {
            var response = await this.Client.PostAsJsonAsync("admin/course/create", courseInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/allCourses");
            }
        }
    }
}
