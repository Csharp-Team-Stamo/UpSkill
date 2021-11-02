namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using global::Data.Models;
    using Infrastructure.Models.Coaches;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<CoachOwner> coachesOwnerRepository;
        private readonly IOwnerService ownerService;

        public CoachesService(IDeletableEntityRepository<Coach> coachesRepository, IDeletableEntityRepository<CoachOwner> coachesOwnerRepository, IOwnerService ownerService)
        {
            this.coachesRepository = coachesRepository;
            this.coachesOwnerRepository = coachesOwnerRepository;
            this.ownerService = ownerService;
        }

        public CoachesListingCatalogModel GetAll(string userId)
        {
            var coaches = new CoachesListingCatalogModel
            {
                OwnerId = ownerService.GetId(userId),
                Coaches = coachesRepository.All().Select(x => new CoachInListCatalogModel
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

        public CoachesListingCatalogModel GetAllByOwnerId(string userId)
        {
            var ownerId = ownerService.GetId(userId);

            var coachesByOwnerId = new CoachesListingCatalogModel
            {
                OwnerId = ownerId,
                Coaches = coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => new CoachInListCatalogModel
                {
                    Id = x.Coach.Id,
                    FullName = x.Coach.FullName,
                    CategoryName = x.Coach.Category.Name,
                    Company = x.Coach.Company,
                    CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                    PricePerSession = x.Coach.PricePerSession,
                }).ToList()
            };

            return coachesByOwnerId;
        }

        public async Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId)
        {
            var coachOwner = new CoachOwner { CoachId = coachId, OwnerId = ownerId, };
            await coachesOwnerRepository.AddAsync(coachOwner);
            await coachesOwnerRepository.SaveChangesAsync();
        }
    }
}
