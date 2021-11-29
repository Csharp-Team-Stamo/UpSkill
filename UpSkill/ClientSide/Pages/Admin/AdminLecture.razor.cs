namespace UpSkill.ClientSide.Pages.Admin
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Course;
    using UpSkill.Infrastructure.Models.Lecture;

    public partial class AdminLecture : ComponentBase
    {
        private LectureCreateInputModel input = new();

        private IEnumerable<AdminCourseListingServiceModel> allCourses =
            new List<AdminCourseListingServiceModel>();

        protected override async Task OnInitializedAsync()
        {
            this.allCourses = await this.Client
                .GetFromJsonAsync<IEnumerable<AdminCourseListingServiceModel>>
                ("/admin/course/all");
        }

        private async Task Create()
        {
            var response = await this.Client
                .PostAsJsonAsync<LectureCreateInputModel>("/admin/lecture/create", input);

            if(response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo($"/watch/course/{input.CourseId}");
            }
        }
    }
}
