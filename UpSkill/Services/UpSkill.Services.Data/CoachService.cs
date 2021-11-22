namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.CoachDescriptionModal;
    using Infrastructure.Models.Coaches;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Common.Pagination.Coaches;

    public class CoachService : ICoachService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<CoachOwner> coachesOwnerRepository;
        private readonly IOwnerService ownerService;

        public CoachService(IDeletableEntityRepository<Coach> coachesRepository, IDeletableEntityRepository<CoachOwner> coachesOwnerRepository, IOwnerService ownerService)
        {
            this.coachesRepository = coachesRepository;
            this.coachesOwnerRepository = coachesOwnerRepository;
            this.ownerService = ownerService;
        }

        public CoachesListingCatalogModel GetAll(string ownerId, CoachesParameters parameters)
        {
            var coachItems = coachesRepository.All()
                .OrderBy(p => p.CreatedOn)
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    CategoryName = x.Category.Name,
                    Company = x.Company,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    PricePerSession = x.PricePerSession,
                })
                .ToList();

            var coaches = new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = new Infrastructure.Common.Pagination.VirtualizeResponse<CoachInListCatalogModel>()
                {
                    Items = coachItems
                }
            };

            return coaches;
        }

        public Task<CoachDescriptionModel> GetByIdAsync(string coachId)
        {
            return coachesRepository.All().Where(x => x.Id == coachId).Select(x => new CoachDescriptionModel
            {
                Id = x.Id,
                FullName = x.FullName,
                CategoryName = x.Category.Name,
                AvatarImgUrl = x.AvatarImgUrl,
                Company = x.Company,
                DiscussionDurationInMinutes = x.DiscussionDurationInMinutes,
                ResourcesCount = x.ResourcesCount,
                SessionDescription = x.SessionDescription,
                SkillsLearn = x.SkillsLearn,
            }).FirstOrDefaultAsync();
        }

        private List<string> OwnerCoachCollectionIds(string ownerId)
        {
            return coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => x.CoachId).ToList();
        }


        public CoachesListingCatalogModel GetAllByOwnerId(string ownerId, CoachesParameters parameters)
        {

           var coachItems = coachesOwnerRepository.All()
                .Where(x => x.OwnerId == ownerId)
                .OrderBy(p => p.CreatedOn)
                .Skip(parameters.StartIndex)
                .Take(parameters.PageSize)
                .Select(x => new CoachInListCatalogModel
                                 {
                                     Id = x.Coach.Id,
                                     FullName = x.Coach.FullName,
                                     CategoryName = x.Coach.Category.Name,
                                     Company = x.Coach.Company,
                                     CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                                     PricePerSession = x.Coach.PricePerSession,
                                 })
                .ToList();

            var coachesByOwnerId = new CoachesListingCatalogModel
            {
                
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = new Infrastructure.Common.Pagination.VirtualizeResponse<CoachInListCatalogModel>()
                {
                    Items = coachItems,
                }
            };

            return coachesByOwnerId;
        }

        public async Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId)
        {
            var coachOwner = new CoachOwner { CoachId = coachId, OwnerId = ownerId, };
            await coachesOwnerRepository.AddAsync(coachOwner);
            await coachesOwnerRepository.SaveChangesAsync();
        }

        public async Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId)
        {
            var coachToRemove = coachesOwnerRepository.All().FirstOrDefault(x => x.CoachId == coachId && x.OwnerId == ownerId);

            coachesOwnerRepository.HardDelete(coachToRemove);

            await coachesOwnerRepository.SaveChangesAsync();
        }
    }
}
