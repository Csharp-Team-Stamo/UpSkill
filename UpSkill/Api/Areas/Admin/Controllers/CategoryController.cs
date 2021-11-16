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
        private readonly IAdminCategoryService categoryService;

        public CategoryController(IAdminCategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CategoryCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Valid category name is required.");
            }

            var category = await this.categoryService.CreateCategory(input);

            if(category == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AdminCategoryListingServiceModel>>> All()
        {
            var allCategories = await this
                .categoryService
                .GetAll();

            if(allCategories.Any() == false)
            {
                return NotFound();
            }

            return new List<AdminCategoryListingServiceModel>
                (allCategories);
        }
    }
}
