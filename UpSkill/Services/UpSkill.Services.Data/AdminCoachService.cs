namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Services.Data.Contracts;

    public class AdminCoachService : IAdminCoachService
    {
        private readonly IAdminCategoryService categoryService;
        private readonly IDeletableEntityRepository<Coach> coachRepo;
        private readonly IDeletableEntityRepository<Language> languageRepo;
        private readonly IDeletableEntityRepository<CoachLanguage> coachLanguagesRepo;

        public AdminCoachService(
            IAdminCategoryService categoryService,
            IDeletableEntityRepository<Coach> coachRepo,
            IDeletableEntityRepository<Language> languageRepo,
            IDeletableEntityRepository<CoachLanguage> coachLanguagesRepo)
        {
            this.categoryService = categoryService;
            this.coachRepo = coachRepo;
            this.languageRepo = languageRepo;
            this.coachLanguagesRepo = coachLanguagesRepo;
        }

        public async Task<Coach> Create(CoachCreateInputModel coachInput)
        {
            var category = await this.categoryService.GetCategory(coachInput.CategoryId);
            var language = await this.languageRepo.All().FirstOrDefaultAsync(l => l.Id == coachInput.LanguageId);

            var coach = CreateCoach(coachInput, category);

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
                Company = coachInDb.Company,
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
                    Price = c.PricePerSession
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

            coachToEdit.FullName = editInput.FullName;
            coachToEdit.AvatarImgUrl = editInput.ImageUrl;
            coachToEdit.PricePerSession = editInput.PricePerSession;
            coachToEdit.Company = editInput.Company;
            
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

            this.coachRepo.Delete(coachToDelete);
            var deleteResult = await this.coachRepo.SaveChangesAsync();

            return deleteResult;
        }

        public async Task<CoachEditInputModel> GetCoachEditModel(string id)
        {
            var coachToEdit = await this.GetCoach(id);

            if(coachToEdit == null)
            {
                return null;
            }

            var editModel = ConvertToEditModel(coachToEdit);

            return editModel;
        }

        public async Task<Coach> GetCoach(string id)
            => await this.coachRepo
                        .All()
                        .Include(c => c.Category)
                        .FirstOrDefaultAsync(c => c.Id == id);

        private async Task<Category> GetCategory(int id)
            => await this.categoryService.GetCategory(id);

        private Coach CreateCoach(CoachCreateInputModel coachInput, Category category)
            => new()
            {
                Category = category,
                CategoryId = category.Id,
                FullName = coachInput.FullName,
                Email = coachInput.Email,
                AvatarImgUrl = coachInput.AvatarImgUrl,
                Company = coachInput.Company,
                CompanyLogoUrl = coachInput.CompanyLogoUrl,
                PricePerSession = coachInput.PricePerSession,
                CalendlyPopupUrl = coachInput.CalendlyPopupUrl,
                SessionDescription = coachInput.SessionDescription,
                DiscussionDurationInMinutes = coachInput.DiscussionDurationInMin,
                SkillsLearn = coachInput.SkillsLearn,
                ResourcesCount = coachInput.ResourcesCount
            };

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
            => new CoachLanguage
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
            => new()
            {
                Id = coachInDb.Id,
                Company = coachInDb.Company,
                FullName = coachInDb.FullName,
                ImageUrl = coachInDb.AvatarImgUrl,
                PricePerSession = coachInDb.PricePerSession,
                CategoryId = coachInDb.Category.Id,
                CategoryName = coachInDb.Category.Name
            };
    }
}
