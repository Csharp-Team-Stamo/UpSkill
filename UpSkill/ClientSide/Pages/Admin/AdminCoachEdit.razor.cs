namespace UpSkill.ClientSide.Pages.Admin
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;

    public partial class AdminCoachEdit : ComponentBase
    {
        private CoachEditInputModel editInput = new();

        public IEnumerable<AdminCategoryListingServiceModel> CategoriesInDb { get; set; } =
            new List<AdminCategoryListingServiceModel>();

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.editInput = await this.Client
                .GetFromJsonAsync<CoachEditInputModel>($"/admin/coach/edit/{Id}");

            this.CategoriesInDb = await this.Client
            .GetFromJsonAsync<IEnumerable<AdminCategoryListingServiceModel>>("/admin/category/all");
        }

        public async Task Edit()
        {
            var response = await this.Client.PutAsJsonAsync("/admin/coach/edit", editInput);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/admin/coach/all");
            }
        }
    }
}
