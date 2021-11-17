namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.ApplicationUser;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> appUserRepository;

        public ApplicationUserService(IDeletableEntityRepository<ApplicationUser> appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public EditApplicationUserModel GetById(string userId)
        {
            return appUserRepository.All().Where(x => x.Id == userId).Select(x => new EditApplicationUserModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                ImageToBase64 = x.ImageToBase64,
                Summary = x.Summary,
            }).FirstOrDefault();
        }

        public async Task UpdateUser(EditApplicationUserModel model)
        {
            var userToUpdate = appUserRepository.All().FirstOrDefault(x => x.Id == model.Id);

            userToUpdate.FullName = model.FullName;
            userToUpdate.ImageToBase64 = model.ImageToBase64;
            userToUpdate.Summary = model.Summary;

            appUserRepository.Update(userToUpdate);
            await appUserRepository.SaveChangesAsync();

        }
    }
}
