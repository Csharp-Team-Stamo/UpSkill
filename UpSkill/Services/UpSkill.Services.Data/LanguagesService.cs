namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languageRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public async Task<ICollection<string>> GetAll()
        {
            return await languageRepository.All().Select(x => x.Name).ToListAsync();
        }
    }
}
