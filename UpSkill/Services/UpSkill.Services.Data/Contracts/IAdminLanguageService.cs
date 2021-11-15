﻿namespace UpSkill.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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