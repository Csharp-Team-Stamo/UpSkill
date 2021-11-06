namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Infrastructure.Models.Language;
    using UpSkill.Services.Data.Contracts;

    public class LanguageController : AdminController
    {
        private readonly IAdminLanguageService languageService;

        public LanguageController(IAdminLanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create(LanguageCreateInputModel input)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Valid language name is required.");
            }

            var createResult = await this.languageService.Create(input);

            if(createResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(204);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<LanguageListingServiceModel>>> All()
        {
            var allLanguages = await this.languageService
                .GetAll();

            return new List<LanguageListingServiceModel>(allLanguages);
        }
    }
}
