﻿namespace UpSkill.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Language;

    public interface IAdminLanguageService
    {
        Task<int?> Create(LanguageCreateInputModel input);
        Task<IEnumerable<LanguageListingServiceModel>> GetAll();
        Task<Language> GetLanguage(int id);
        Task<LanguageListingServiceModel> GetLanguageDetails(int id);
    }
}
