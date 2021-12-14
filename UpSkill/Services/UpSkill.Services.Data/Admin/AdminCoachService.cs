namespace UpSkill.Services.Data.Admin
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Models.Category;
    using Infrastructure.Models.Coach;
    using Contracts;
    using UpSkill.Services.Data.Contracts;

    public class AdminCoachService : IAdminCoachService
    {
        private readonly IAdminCategoryService categoryService;
        private readonly IDeletableEntityRepository<Coach> coachRepo;
        private readonly IDeletableEntityRepository<Language> languageRepo;
        private readonly IDeletableEntityRepository<CoachEmployee> coachEmployeeRepo;
        private readonly IDeletableEntityRepository<CoachLanguage> coachLanguagesRepo;
        private readonly IDeletableEntityRepository<CoachOwner> coachOwnerRepo;
        private readonly IDeletableEntityRepository<LiveSession> sessionRepo;
        private readonly IimagesService iimagesService;

        public AdminCoachService(
            IAdminCategoryService categoryService,
            IDeletableEntityRepository<Coach> coachRepo,
            IDeletableEntityRepository<Language> languageRepo,
            IDeletableEntityRepository<CoachEmployee> coachEmployeeRepo,
            IDeletableEntityRepository<CoachLanguage> coachLanguagesRepo,
            IDeletableEntityRepository<CoachOwner> coachOwnerRepo,
            IDeletableEntityRepository<LiveSession> sessionRepo,
            IimagesService iimagesService)
        {
            this.categoryService = categoryService;
            this.coachRepo = coachRepo;
            this.languageRepo = languageRepo;
            this.coachEmployeeRepo = coachEmployeeRepo;
            this.coachLanguagesRepo = coachLanguagesRepo;
            this.coachOwnerRepo = coachOwnerRepo;
            this.sessionRepo = sessionRepo;
            this.iimagesService = iimagesService;
        }

        public async Task<Coach> Create(CoachCreateInputModel coachInput)
        {
            var coachExists = await this.CoachExists(
                coachInput.Email, coachInput.FullName, coachInput.SessionDescription);

            if(coachExists)
            {
                return null;
            }

            var category = await this.categoryService
                                     .GetCategory(coachInput.CategoryId);

            var language = await this.languageRepo
                                     .All()
                                     .FirstOrDefaultAsync(l => l.Id == coachInput.LanguageId);

            var coach = await CreateCoach(coachInput, category);

            await this.coachRepo.AddAsync(coach);

            var createResult = await this.coachRepo.SaveChangesAsync();

            if(createResult <= 0)
            {
                return null;
            }

            var addLangResTEMP = await this.AddLanguageTEMP(coach, language);
            // var addLanguagesResult = await this.AddLanguages(coach, coachInput.Languages);

            return addLangResTEMP <= 0 ?
                null :
                coach;
        }

        private async Task<bool> CoachExists(
            string email,
            string fullName,
            string sessionDescription)
        {
            var coachWithEmail = await this.coachRepo
                .All()
                .FirstOrDefaultAsync(c => 
                    c.Email == email && 
                    c.FullName == fullName && 
                    c.SessionDescription == sessionDescription);

            return coachWithEmail != null;
        }
        public async Task<CoachDetailsServiceModel> GetCoachDetails(string id)
        {
            var coachInDb = await this.GetCoach(id);

            if(coachInDb == null)
            {
                return null;
            }

            var coachDetails = new CoachDetailsServiceModel
            {
                Id = coachInDb.Id,
                FullName = coachInDb.FullName,
                ImageUrl = coachInDb.AvatarImgUrl,
                Category = new CategoryDetailsServiceModel
                {
                    Id = coachInDb.Category.Id,
                    Name = coachInDb.Category.Name
                },
                Company = coachInDb.CompanyName,
                PricePerSession = coachInDb.PricePerSession
            };

            return coachDetails;
        }

        public async Task<IEnumerable<AdminCoachListingServiceModel>> GetAll()
        {
            var allCoaches = await this.coachRepo
                .All()
                .OrderBy(c => c.FullName)
                .Select(c => new AdminCoachListingServiceModel
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    CategoryName = c.Category.Name,
                    CompanyLogoUrl = c.CompanyLogoUrl,
                    Price = c.PricePerSession,
                    AvatarUrl = c.AvatarImgUrl
                })
                .ToListAsync();

            return allCoaches;
        }

        public async Task<int?> ExecuteEdit(CoachEditInputModel editInput)
        {
            var coachToEdit = await this.GetCoach(editInput.Id);

            if(coachToEdit == null)
            {
                return null;
            }

            ImplementEdits(coachToEdit, editInput);

            if(coachToEdit.AvatarImgUrl != editInput.AvatarImgUrl)
            {
                coachToEdit.AvatarImgUrl = await this.iimagesService
                    .RemoveImgBackground(editInput.AvatarImgUrl);
            }
            
            if(coachToEdit.Category.Id != editInput.CategoryId)
            {
                coachToEdit.Category = await this.GetCategory(editInput.CategoryId);
                coachToEdit.CategoryId = editInput.CategoryId;
            }

            this.coachRepo.Update(coachToEdit);
            var editResult = await this.coachRepo.SaveChangesAsync();

            return editResult;
        }

        public async Task<int?> Delete(string id)
        {
            var coachToDelete = await this.GetCoach(id);

            if(coachToDelete == null)
            {
                return null;
            }

            await DeleteRecordsInCoachEmployeesTable(coachToDelete.Id);
            await DeleteRecordsInCoachLanguagesTable(coachToDelete.Id);
            await DeleteRecordsInCoachOwnersTable(coachToDelete.Id);
            await DeleteCoachLiveSession(coachToDelete.Id);
            this.coachRepo.Delete(coachToDelete);

            var deleteResult = await this.coachRepo.SaveChangesAsync();

            return deleteResult;
        }

        //private async Task DeleteCoachInCoachVotesTable(string coachId)
        //{
        //    var coachVotes = await this.coachVotesRepo
        //        .All()
        //        .Where(cv => cv.CoachId == coachId)
        //        .ToListAsync();

        //    coachVotes.ForEach(cv => this.coachVotesRepo.Delete(cv));

        //    await this.coachVotesRepo.SaveChangesAsync();
        //}

        private void ImplementEdits(Coach coachToEdit, CoachEditInputModel editInput)
        {
            coachToEdit.FullName = editInput.FullName;
            coachToEdit.Email = editInput.Email;
            coachToEdit.CompanyName = editInput.CompanyName;
            coachToEdit.CompanyLogoUrl = editInput.CompanyLogoUrl;
            coachToEdit.VideoUrl = editInput.VideoUrl;
            coachToEdit.SessionDescription = editInput.SessionDescription;
            coachToEdit.SkillsLearn = editInput.SkillsLearn;
            coachToEdit.DiscussionDurationInMinutes = editInput.DiscussionDurationInMin;
            coachToEdit.ResourcesCount = editInput.ResourcesCount;
            coachToEdit.PricePerSession = editInput.PricePerSession;
            coachToEdit.CalendlyPopupUrl = editInput.CalendlyPopupUrl;
        }
        private async Task DeleteCoachLiveSession(string coachId)
        {
            var liveSession = await this.sessionRepo
                .All()
                .FirstOrDefaultAsync(s => s.Coach.Id == coachId);

            if(liveSession == null)
            {
                return;
            }

            this.sessionRepo.Delete(liveSession);
            await this.sessionRepo.SaveChangesAsync();
        }

        private async Task DeleteRecordsInCoachEmployeesTable(string coachId)
        {
            var coachEmployees = await this.coachEmployeeRepo
                .All()
                .Where(ce => ce.Coach.Id == coachId)
                .ToListAsync();

            if(coachEmployees.Any() == false)
            {
                return;
            }

            coachEmployees.ForEach(ce => this.coachEmployeeRepo.Delete(ce));

            await this.coachEmployeeRepo.SaveChangesAsync();
        }

        private async Task DeleteRecordsInCoachLanguagesTable(string coachId)
        {
            var coachLanguages = await this.coachLanguagesRepo
                .All()
                .Where(cl => cl.Coach.Id == coachId)
                .ToListAsync();

            if(coachLanguages.Any() == false)
            {
                return;
            }

            coachLanguages.ForEach(cl => this.coachLanguagesRepo.Delete(cl));

            await this.coachLanguagesRepo.SaveChangesAsync();
        }

        private async Task DeleteRecordsInCoachOwnersTable(string coachId)
        {
            var coachOwners = await this.coachOwnerRepo
                .All()
                .Where(co => co.Coach.Id == coachId)
                .ToListAsync();

            if(coachOwners.Any() == false)
            {
                return;
            }

            coachOwners.ForEach(co => this.coachOwnerRepo.Delete(co));

            await this.coachOwnerRepo.SaveChangesAsync();
        }
        public async Task<CoachEditInputModel> GetCoachEditModel(string id)
        {
            var coachToEdit = await this.GetCoach(id);

            if(coachToEdit == null)
            {
                return null;
            }

            var editModel = ConvertToEditModel(coachToEdit);

            var coachLanguage = await this.coachLanguagesRepo
                .All()
                .SingleOrDefaultAsync(cl => cl.CoachId == id);

            editModel.LanguageId = coachLanguage.LanguageId;

            return editModel;
        }

        public async Task<Coach> GetCoach(string id)
            => await this.coachRepo
                        .All()
                        .Include(c => c.Category)
                        .FirstOrDefaultAsync(c => c.Id == id);

        private async Task<Category> GetCategory(int id)
            => await this.categoryService.GetCategory(id);

        private async Task<Coach> CreateCoach(CoachCreateInputModel coachInput, Category category)
        {
            var noBGimage = await this
                .iimagesService
                .RemoveImgBackground(coachInput.AvatarImgUrl);

            return new()
            {
                Category = category,
                CategoryId = category.Id,
                FullName = coachInput.FullName,
                Email = coachInput.Email,
                AvatarImgUrl = noBGimage,
                CompanyName = coachInput.CompanyName,
                CompanyLogoUrl = coachInput.CompanyLogoUrl,
                PricePerSession = coachInput.PricePerSession,
                CalendlyPopupUrl = coachInput.CalendlyPopupUrl,
                SessionDescription = coachInput.SessionDescription,
                DiscussionDurationInMinutes = coachInput.DiscussionDurationInMin,
                SkillsLearn = coachInput.SkillsLearn,
                ResourcesCount = coachInput.ResourcesCount,
                VideoUrl = coachInput.VideoUrl
            };
        }

        private async Task<int> AddLanguageTEMP(Coach coach, Language language)
        {
            await this.coachLanguagesRepo
            .AddAsync(
                new CoachLanguage
                {
                    Coach = coach,
                    CoachId = coach.Id,
                    Language = language,
                    LanguageId = language.Id
                });

            return await this.coachLanguagesRepo.SaveChangesAsync();
        }

        //private async Task<int> AddLanguages(Coach coach, ICollection<int> languageIds)
        //{
        //    var coachLanguages = await this.GetCoachLanguages(languageIds);

        //    foreach(var language in coachLanguages)
        //    {
        //        var coachLang = CreateCoachLanguageEntity(coach, language);

        //        await this.coachLanguagesRepo.AddAsync(coachLang);
        //    }

        //    return await this.coachLanguagesRepo.SaveChangesAsync();
        //}

        private CoachLanguage CreateCoachLanguageEntity(Coach coach, Language language)
            => new ()
            {
                Language = language,
                LanguageId = language.Id,
                Coach = coach,
                CoachId = coach.Id
            };

        private async Task<IEnumerable<Language>> GetCoachLanguages(ICollection<int> languageIds)
            => await this.languageRepo
                .All()
                .Where(l => languageIds.Contains(l.Id))
                .ToListAsync();

        private static CoachEditInputModel ConvertToEditModel(Coach coachInDb)
        {
            var editInput = new CoachEditInputModel
            {
                Id = coachInDb.Id,
                FullName = coachInDb.FullName,
                AvatarImgUrl = coachInDb.AvatarImgUrl,
                Email = coachInDb.Email,
                CompanyName = coachInDb.CompanyName,
                CompanyLogoUrl = coachInDb.CompanyLogoUrl,
                VideoUrl = coachInDb.VideoUrl,
                SessionDescription = coachInDb.SessionDescription,
                SkillsLearn = coachInDb.SkillsLearn,
                DiscussionDurationInMin = coachInDb.DiscussionDurationInMinutes,
                ResourcesCount = coachInDb.ResourcesCount,
                PricePerSession = coachInDb.PricePerSession,
                CalendlyPopupUrl = coachInDb.CalendlyPopupUrl,
                CategoryId = coachInDb.CategoryId
            };

            return editInput;
        }
    }
}
