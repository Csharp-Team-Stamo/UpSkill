using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using UpSkill.Services.Data.Contracts;

namespace UpSkill.Services.Data
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration configuration;

        public CloudinaryService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Cloudinary GetCloudinaryClient()
        {
            Account account = new(
                configuration["ExternalProviders:Cloudinary:CloudName"],
                configuration["ExternalProviders:Cloudinary:ApiKey"],
                configuration["ExternalProviders:Cloudinary:ApiSecret"]);

            return new (account);

        }
    }
}
