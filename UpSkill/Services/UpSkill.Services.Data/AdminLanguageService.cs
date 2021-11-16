namespace UpSkill.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Language;
    using UpSkill.Services.Data.Contracts;

    public class AdminLanguageService : IAdminLanguageService
    {
        private readonly IDeletableEntityRepository<Language> languageRepo;

        public AdminLanguageService(IDeletableEntityRepository<Language> languageRepo)
        {
            this.languageRepo = languageRepo;
        }

        public async Task<int?> Create(LanguageCreateInputModel input)
        {
            var language = new Language
            {
                Name = input.Name
            };

            await this.languageRepo.AddAsync(language);
            var createResult = await this.languageRepo.SaveChangesAsync();

            return createResult <= 0 ?
                null :
                language.Id;
        }

        public async Task<IEnumerable<LanguageListingServiceModel>> GetAll()
        {
            var allLanguages = await this.languageRepo
                .All()
                .Select(l => new LanguageListingServiceModel
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .ToListAsync();

            return allLanguages;
        }

        public async Task<Language> GetLanguage(int id)
        {
            var language = await this.languageRepo
                .All()
                .FirstOrDefaultAsync(l => l.Id == id);

            return language;
        }

        public async Task<LanguageListingServiceModel> GetLanguageDetails(int id)
        {
            var language = await this.languageRepo
                .All()
                .Select(l => new LanguageListingServiceModel
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .FirstOrDefaultAsync(l => l.Id == id);

            return language;
        }
    }
}
