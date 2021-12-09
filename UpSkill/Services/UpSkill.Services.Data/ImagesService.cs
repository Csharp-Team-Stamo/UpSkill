namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UpSkill.Services.Data.Contracts;
    public class ImagesService : IimagesService
    {
        public async Task<string> RemoveImgBackground(string url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://background-removal.p.rapidapi.com/remove"),
                Headers =
                           {
                               { "x-rapidapi-host", "background-removal.p.rapidapi.com" },
                               { "x-rapidapi-key", "2f76c9f9f4msh5cc1987d48e8820p184929jsn59aaeadeea6c" },
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
