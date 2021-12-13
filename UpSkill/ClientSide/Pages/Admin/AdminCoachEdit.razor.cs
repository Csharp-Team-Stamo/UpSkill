namespace UpSkill.ClientSide.Pages.Admin
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Infrastructure.Models.Language;

    public partial class AdminCoachEdit : ComponentBase
    {
        private CoachEditInputModel editInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb { get; set; } =
            new List<AdminCategoryListingServiceModel>();

        public IEnumerable<LanguageListingServiceModel> LanguagesInDb { get; set; } =
            new List<LanguageListingServiceModel>();

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.editInput = await this.Client
                .GetFromJsonAsync<CoachEditInputModel>
                ($"/admin/coach/edit/{Id}");

            this.CategoriesInDb = await this.Client
            .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>
            ("/admin/category/all");

            this.LanguagesInDb = await this.Client
                .GetFromJsonAsync<IEnumerable<LanguageListingServiceModel>>
                ("/admin/language/all");

            Console.WriteLine(string.Join(", ", this.LanguagesInDb));
        }

        public async Task Edit()
        {
            var response = await this.Client
                .PutAsJsonAsync("/admin/coach/edit", editInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/coach/all");
            }
        }
    }
}
