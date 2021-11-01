namespace UpSkill.Services.Data
{
    using System.Linq;
    using Contracts;
    using Infrastructure.Models.Coaches;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;

        public CoachesService(IDeletableEntityRepository<Coach> coachesRepository)
        {
            this.coachesRepository = coachesRepository;
        }

        public CoachesListingCatalogModel GetAll()
        {
            var coaches = new CoachesListingCatalogModel
            {
                Coaches = coachesRepository.All().Select(x => new CoachInListingCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    CategoryName = x.Category.Name,
                    Company = x.Company,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    PricePerSession = x.PricePerSession,
                }).ToList()
            };

            return coaches;
        }
    }
}
