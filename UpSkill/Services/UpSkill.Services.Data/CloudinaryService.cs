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
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]);

            return new (account);

        }
    }
}
