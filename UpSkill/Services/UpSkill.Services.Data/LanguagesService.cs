namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languageRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public ICollection<string> GetAll()
        {
            return languageRepository.All().Select(x => x.Name).ToList();
        }
    }
}
