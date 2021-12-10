namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UpSkill.Services.Data.Contracts;
    public class ImagesService : IimagesService
    {
        private readonly IConfiguration configuration;

        public ImagesService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> RemoveImgBackground(string url)
        {
            var client = new HttpClient();
            var host = configuration["ExternalProviders:RapidAPI:Host"];
            var key = configuration["ExternalProviders:RapidAPI:RemoveBGKey"];

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://background-removal.p.rapidapi.com/remove"),
                Headers =
                           {
                               { "x-rapidapi-host", host },
                               { "x-rapidapi-key", key },
                           },
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                           {
                               { "image_url", $"{url}" },
                           }),
            };

            var imgUrl = string.Empty;

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                imgUrl = JObject.Parse(body)["response"]["image_url"].ToString();
            }

            return imgUrl;
        }
    }
}
