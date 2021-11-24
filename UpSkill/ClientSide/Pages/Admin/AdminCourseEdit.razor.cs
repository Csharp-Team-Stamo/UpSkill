﻿namespace UpSkill.ClientSide.Pages.Admin
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Course;

    public partial class AdminCourseEdit : ComponentBase
    {
        private CourseEditInputModel editModel = new();

        [Parameter]
        public int Id { get; set; }

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb { get; set; } =
            new List<AdminCategoryListingServiceModel>();

        protected override async Task OnInitializedAsync()
        {
            this.editModel = await this.Client
                .GetFromJsonAsync<CourseEditInputModel>
                ($"/admin/course/edit/{Id}");

            this.CategoriesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>
                ("/admin/category/all");
        }

        public async Task Edit()
        {
            var response = await this.Client
                .PutAsJsonAsync<CourseEditInputModel>
                ("/admin/course/edit", editModel);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/course/all");
            }
        }
    }
}
