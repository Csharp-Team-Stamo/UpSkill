﻿namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Coach;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<CoachOwner> coachesOwnerRepository;

        public CoachesService(IDeletableEntityRepository<Coach> coachesRepository, IDeletableEntityRepository<CoachOwner> coachesOwnerRepository)
        {
            this.coachesRepository = coachesRepository;
            this.coachesOwnerRepository = coachesOwnerRepository;
        }

        public CoachesListingCatalogModel GetAll(string ownerId)
        {
            var coaches = new CoachesListingCatalogModel
            {
                OwnerId = ownerId,
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = coachesRepository.All().Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ImageUrl = x.AvatarImgUrl,
                    CategoryName = x.Category.Name,
                    Company = x.Company,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    PricePerSession = x.PricePerSession,
                    Languages = x.Languages.Select(cl => cl.Language.Name).ToList(),
                }).ToList()
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

        public CoachesListingCatalogModel GetAllByOwnerId(string ownerId)
        {

            var coachesByOwnerId = new CoachesListingCatalogModel
            {
                OwnerId = ownerId,
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
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

        public async Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId)
        {
            var coachToRemove = coachesOwnerRepository.All().FirstOrDefault(x => x.CoachId == coachId && x.OwnerId == ownerId);

            coachesOwnerRepository.HardDelete(coachToRemove);

            await coachesOwnerRepository.SaveChangesAsync();
        }

        private List<string> OwnerCoachCollectionIds(string ownerId)
        {
            return coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId)
                .Select(x => x.CoachId)
                .ToList();
        }
    }
}
