namespace UpSkill.Services.Data
{
    using System.Linq;
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
    }
}
