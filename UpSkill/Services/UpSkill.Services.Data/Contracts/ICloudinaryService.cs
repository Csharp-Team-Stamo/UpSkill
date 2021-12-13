using CloudinaryDotNet;

namespace UpSkill.Services.Data.Contracts
{
    public interface ICloudinaryService
    {
        Cloudinary GetCloudinaryClient();
    }
}
