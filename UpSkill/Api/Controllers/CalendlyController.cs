namespace UpSkill.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class CalendlyController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CalendlyController()
        {
        }

        [HttpPost("InitializeCalendlyWebhook")]
        public async Task<IActionResult> InitializeCalendlyWebhook()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
                        {
                            Method = HttpMethod.Post,
                            RequestUri = new Uri("https://api.calendly.com/webhook_subscriptions"),
                            Headers =
                                     {
                                         { "Content-Type", "application/json" },
                                         { "Authorization", "" },
                                     },
                            
                            Content = new StringContent("{\n  \"url\": \"https://blah.foo/bar\",\n  \"events\": [\n    \"invitee.created\",\n               \"invitee.canceled\"\n  ],\n  \"organization\": \"https://api.calendly.com/organizations/AAAAAAAAAAAAAAAA\",\n  \"user\":       \"https://   api.calendly.com/users/BBBBBBBBBBBBBBBB\",\n  \"scope\": \"user\",\n  \"signing_key\": \"5mEzn9C-      I28UtwOjZJtFoob0sAAFZ95GbZkqj4y3i0I   \"\n}")
                            {
                                Headers =
                                         {
                                             ContentType = new MediaTypeHeaderValue("application/json")
                                         }
                            }
                        };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            return StatusCode(200);
        }
    }
}
