namespace UpSkill.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public ICollection<string> GetAllNames()
        {
            return categoriesRepository.All().Select(x => x.Name).ToList();
        }
    }
}
