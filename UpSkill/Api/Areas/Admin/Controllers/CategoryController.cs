namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Services.Data.Contracts;

    public class CategoryController : AdminController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CategoryCreateInputModel>>> All()
        {
            var allCategories = await this.categoryService.GetAll();

            return new List<CategoryCreateInputModel>(allCategories);
        }
    }
}
