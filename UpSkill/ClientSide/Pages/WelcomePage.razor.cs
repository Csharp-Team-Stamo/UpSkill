namespace UpSkill.ClientSide.Pages
{
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using UpSkill.Infrastructure.Models.WelcomePage;

    public partial class WelcomePage
    {
        private readonly RequestDemoFromModel input = new();

        [Inject]
        public HttpClient client { get; set; }

        public async Task RequestDemo()
        {
            var content = JsonSerializer.Serialize(this.input);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var requestResult = await client.PostAsync("welcomePage/requestDemo", bodyContent);

            if(requestResult.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/demoSuccess");
            }
            else
            {
                NavigationManager.NavigateTo("/demoFail");
            }
        }
    }
}
