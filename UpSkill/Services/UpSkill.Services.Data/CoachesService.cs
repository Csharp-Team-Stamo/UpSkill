﻿namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Infrastructure.Models.Coach;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Common.Pagination;
    using UpSkill.Infrastructure.Models.Dashboard;
    using UpSkill.Services.Data.Paging;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<CoachOwner> coachesOwnerRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IDeletableEntityRepository<LiveSession> liveSessionsRepository;

        public CoachesService(
            IDeletableEntityRepository<Coach> coachesRepository,
            IDeletableEntityRepository<CoachOwner> coachesOwnerRepository,
            IDeletableEntityRepository<Employee> employeeRepository,
            IDeletableEntityRepository<LiveSession> liveSessionsRepository)
        {
            this.coachesRepository = coachesRepository;
            this.coachesOwnerRepository = coachesOwnerRepository;
            this.employeeRepository = employeeRepository;
            this.liveSessionsRepository = liveSessionsRepository;
        }

        public Task<CoachDescriptionModel> GetByIdAsync(string coachId)
        {

            return coachesRepository.All().Where(x => x.Id == coachId).Select(x => new CoachDescriptionModel
            {
                Id = x.Id,
                FullName = x.FullName,
                CategoryName = x.Category.Name,
                AvatarImgUrl = x.AvatarImgUrl,
                Company = x.CompanyName,
                VideoUrl = x.VideoUrl,
                DiscussionDurationInMinutes = x.DiscussionDurationInMinutes,
                ResourcesCount = x.ResourcesCount,
                SessionDescription = x.SessionDescription,
                SkillsLearn = x.SkillsLearn,
            }).FirstOrDefaultAsync();
        }

        public CoachesListingCatalogModel GetAllByOwnerId(string ownerId)
        {
             var result = new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => new CoachInListCatalogModel
                {
                    Id = x.Coach.Id,
                    FullName = x.Coach.FullName,
                    CategoryName = x.Coach.Category.Name,
                    Company = x.Coach.CompanyName,
                    CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                    CalendlyUrl = x.Coach.CalendlyPopupUrl,
                    PricePerSession = x.Coach.PricePerSession,
                }).ToList()
            };
            return result;
        }

        public CoachesListingCatalogModel GetAllByEmployeeId(string ownerId, string userId)
        {
            var employeeId = employeeRepository.All().FirstOrDefault(x => x.UserId == userId).Id;

            var result = new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => new CoachInListCatalogModel
                {
                    Id = x.Coach.Id,
                    FullName = x.Coach.FullName,
                    CategoryName = x.Coach.Category.Name,
                    Company = x.Coach.CompanyName,
                    CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                    CalendlyUrl = x.Coach.CalendlyPopupUrl,
                    PricePerSession = x.Coach.PricePerSession,
                    IsFeedbackNeeded = x.Coach.LiveSessions.Any(x => x.StudentId == employeeId && x.GivenFeedback == false),
                    IsCoachSessionPending = x.Coach.LiveSessions.Any(x => x.StudentId == employeeId && x.Start > System.DateTime.UtcNow),
                    IsNotFirstCoachSession = x.Coach.LiveSessions.Any(x => x.StudentId == employeeId)
                }).ToList()
            };

            return result;
        }

        public async Task<ICollection<CoachInListCatalogModel>> GetAllWithExistingSessions(string employeeId)
        {
            return await this.coachesRepository.All()
                .Where(ls => ls.LiveSessions.Any(ls => ls.StudentId == employeeId))
                .Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    CategoryName = x.Category.Name,
                    Company = x.CompanyName,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    CalendlyUrl = x.CalendlyPopupUrl,
                    PricePerSession = x.PricePerSession,
                    IsFeedbackNeeded = x.LiveSessions.Any(x => x.StudentId == employeeId && x.GivenFeedback == false),
                    IsCoachSessionPending = x.LiveSessions.Any(x => x.StudentId == employeeId && x.Start > System.DateTime.UtcNow),
                    IsNotFirstCoachSession = x.LiveSessions.Any(x => x.StudentId == employeeId)
                })
                .ToListAsync();
        }

        public CoachesListingCatalogModel GetAll(string ownerId)
        {
            return new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = OwnerCoachCollectionIds(ownerId),
                Coaches = coachesRepository.All().Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ImageUrl = x.AvatarImgUrl,
                    CategoryName = x.Category.Name,
                    Company = x.CompanyName,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    PricePerSession = x.PricePerSession,
                    Languages = x.Languages.Select(cl => cl.Language.Name).ToList(),
                }).ToList()
            };
        }

        public async Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId)
        {
            var coachOwner = new CoachOwner { CoachId = coachId, OwnerId = ownerId, };
            await coachesOwnerRepository.AddAsync(coachOwner);
            await coachesOwnerRepository.SaveChangesAsync();
        }

        public async Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string ownerId)
        {
            var coachToRemove = await coachesOwnerRepository.All()
                .FirstOrDefaultAsync(x => x.CoachId == coachId && x.OwnerId == ownerId);
            coachesOwnerRepository.HardDelete(coachToRemove);
            await coachesOwnerRepository.SaveChangesAsync();
        }

        private List<string> OwnerCoachCollectionIds(string ownerId)
        {
            return coachesOwnerRepository.All()
                .Where(x => x.OwnerId == ownerId)
                .Select(x => x.CoachId)
                .ToList();
        }

        public PagedList<CoachDashboardStatItemModel> GetDashboardCoaches(string ownerId, int month, TableEntityParameters parameters)
        {
            var coaches = this.liveSessionsRepository.All()
             .Where(x => x.Student.OwnerId == ownerId && x.End.Month == month)
             .GroupBy(x => x.Coach.FullName)
             .Select(x => new CoachDashboardStatItemModel { Name = x.Key, Sessions = x.Count() })
             .ToList();

            return PagedList<CoachDashboardStatItemModel>.ToPagedList(coaches, parameters.PageNumber, parameters.PageSize);
        }
    }
}
