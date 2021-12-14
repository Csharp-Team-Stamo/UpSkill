namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.ApplicationUser;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> appUserRepository;

        public ApplicationUserService(IDeletableEntityRepository<ApplicationUser> appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public async Task<EditApplicationUserModel> GetByIdAsync(string userId)
        {
            return await appUserRepository.All().Where(x => x.Id == userId).Select(x => new EditApplicationUserModel
            {
                Id = x.Id,
                FullName = x.FullName,
                CompanyName = x.Company.Name,
                Email = x.Email,
                ImageToBase64 = x.ImageToBase64,
                Summary = x.Summary,
            }).FirstOrDefaultAsync();
        }

        public async Task UpdateUserAsync(EditApplicationUserModel model)
        {
            var userToUpdate = await appUserRepository.All().FirstOrDefaultAsync(x => x.Id == model.Id);

            userToUpdate.FullName = model.FullName;
            userToUpdate.ImageToBase64 = model.ImageToBase64;
            userToUpdate.Summary = model.Summary;

            appUserRepository.Update(userToUpdate);
            await appUserRepository.SaveChangesAsync();

        }
    }
}
