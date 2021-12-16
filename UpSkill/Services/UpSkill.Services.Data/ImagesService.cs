namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json.Linq;
    using Contracts;
    public class ImagesService : IimagesService
    {
        private readonly IConfiguration configuration;
        private readonly ICloudinaryService cloudinaryService;

        public ImagesService(
            IConfiguration configuration,
            ICloudinaryService cloudinaryService)
        {
            this.configuration = configuration;
            this.cloudinaryService = cloudinaryService;
        }
        public async Task<string> RemoveImgBackground(string url)
        {

            var imgName = url.Split("/", StringSplitOptions.None).Last();
            var dateAdded = DateTime.UtcNow.ToString("d", CultureInfo.GetCultureInfo("nl-NL"));
            var name = imgName + dateAdded;


            var cloudinaryClient = cloudinaryService.GetCloudinaryClient();

            var client = new HttpClient();
            var host = configuration["ExternalProviders:RapidAPI:Host"];
            var key = configuration["ExternalProviders:RapidAPI:RemoveBGKey"];

            var request = new HttpRequestMessage
            {
                Method = System.Net.Http.HttpMethod.Post,
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

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription($"{imgUrl}"),
                PublicId = name
            };

            var urlResult = cloudinaryClient
                .Upload(uploadParams)
                .SecureUrl
                .ToString();

            return urlResult;
        }
    }
}
