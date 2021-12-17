namespace UpSkill.Services.Data
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
    using Infrastructure.Common.Pagination;
    using Infrastructure.Models.Dashboard;
    using Paging;

    public class CoachesService : ICoachesService
    {
        private readonly IDeletableEntityRepository<Coach> coachesRepository;
        private readonly IDeletableEntityRepository<CoachOwner> coachesOwnerRepository;
        private readonly IDeletableEntityRepository<LiveSession> liveSessionsRepository;
        private readonly IEmployeesService employeesService;

        public CoachesService(
            IDeletableEntityRepository<Coach> coachesRepository,
            IDeletableEntityRepository<CoachOwner> coachesOwnerRepository,
            IDeletableEntityRepository<LiveSession> liveSessionsRepository
            , IEmployeesService employeesService)
        {
            this.coachesRepository = coachesRepository;
            this.coachesOwnerRepository = coachesOwnerRepository;
            this.liveSessionsRepository = liveSessionsRepository;
            this.employeesService = employeesService;
        }

        public async Task<CoachDescriptionModel> GetByIdAsync(string coachId)
        {

            return await coachesRepository.All().Where(x => x.Id == coachId).Select(x => new CoachDescriptionModel
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

        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync(string ownerId)
        {
            var result = new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = await GetOwnerCoachCollectionIdsAsync(ownerId),
                Coaches = await coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => new CoachInListCatalogModel
                {
                    Id = x.Coach.Id,
                    FullName = x.Coach.FullName,
                    ImageUrl = x.Coach.AvatarImgUrl,
                    CategoryName = x.Coach.Category.Name,
                    Company = x.Coach.CompanyName,
                    CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                    CalendlyUrl = x.Coach.CalendlyPopupUrl,
                    PricePerSession = x.Coach.PricePerSession,
                }).ToListAsync()
            };

            return result;
        }

        public async Task<CoachesListingCatalogModel> GetAllByEmployeeIdAsync(string ownerId, string userId)
        {
            var employeeId = await employeesService.GetEmployeeIdByAppUserIdAsync(userId);

            var result = new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = await GetOwnerCoachCollectionIdsAsync(ownerId),
                Coaches = await coachesOwnerRepository.All().Where(x => x.OwnerId == ownerId).Select(x => new CoachInListCatalogModel
                {
                    Id = x.Coach.Id,
                    FullName = x.Coach.FullName,
                    ImageUrl = x.Coach.AvatarImgUrl,
                    CategoryName = x.Coach.Category.Name,
                    Company = x.Coach.CompanyName,
                    CompanyLogoUrl = x.Coach.CompanyLogoUrl,
                    CalendlyUrl = x.Coach.CalendlyPopupUrl,
                    PricePerSession = x.Coach.PricePerSession,
                    IsFeedbackNeeded = x.Coach.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId && liveSession.GivenFeedback == false),
                    IsCoachSessionPending = x.Coach.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId && liveSession.Start > DateTime.UtcNow),
                    IsNotFirstCoachSession = x.Coach.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId)
                }).ToListAsync()
            };

            return result;
        }

        public async Task<ICollection<CoachInListCatalogModel>> GetAllWithExistingSessionsAsync(string employeeId)
        {
            return await this.coachesRepository.All()
                .Where(c => c.LiveSessions.Any(ls => ls.StudentId == employeeId))
                .Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    CategoryName = x.Category.Name,
                    ImageUrl = x.AvatarImgUrl,
                    Company = x.CompanyName,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    CalendlyUrl = x.CalendlyPopupUrl,
                    PricePerSession = x.PricePerSession,
                    IsFeedbackNeeded = x.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId && liveSession.GivenFeedback == false),
                    IsCoachSessionPending = x.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId && liveSession.Start > DateTime.UtcNow),
                    IsNotFirstCoachSession = x.LiveSessions.Any(liveSession => liveSession.StudentId == employeeId)
                })
                .ToListAsync();
        }

        public async Task<CoachesListingCatalogModel> GetAllAsync(string ownerId)
        {
            return new CoachesListingCatalogModel
            {
                OwnerCoachCollectionIds = await GetOwnerCoachCollectionIdsAsync(ownerId),
                Coaches = await coachesRepository.All().Select(x => new CoachInListCatalogModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    ImageUrl = x.AvatarImgUrl,
                    CategoryName = x.Category.Name,
                    Company = x.CompanyName,
                    CompanyLogoUrl = x.CompanyLogoUrl,
                    PricePerSession = x.PricePerSession,
                    Languages = x.Languages.Select(cl => cl.Language.Name).ToList(),
                }).ToListAsync()
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

        public async Task<PagedList<CoachDashboardStatItemModel>> GetDashboardCoaches(string ownerId, int month,
            TableEntityParameters parameters)
        {
            var coaches = await this.liveSessionsRepository.All()
             .Where(x => x.Student.OwnerId == ownerId && x.End.Month == month)
             .GroupBy(x => x.Coach.FullName)
             .Select(x => new CoachDashboardStatItemModel { Name = x.Key, Sessions = x.Count() })
             .ToListAsync();

            return PagedList<CoachDashboardStatItemModel>.ToPagedList(coaches, parameters.PageNumber, parameters.PageSize);
        }

        private Task<List<string>> GetOwnerCoachCollectionIdsAsync(string ownerId)
        {
            return coachesOwnerRepository.All()
                .Where(x => x.OwnerId == ownerId)
                .Select(x => x.CoachId)
                .ToListAsync();
        }
    }
}
