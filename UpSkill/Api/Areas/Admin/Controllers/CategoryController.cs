namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models.Category;
    using UpSkill.Services.Data.Admin.Contracts;

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
        public async Task<ActionResult<IEnumerable<AdminCategoryListingServiceModel>>> 
            All()
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

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult<CategoryEditInputModel>> Edit(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Valid Category Id is required.");
            }

            var categoryToEdit = await this.categoryService
                .GetCategoryToEdit(id);

            if(categoryToEdit == null)
            {
                return NotFound($"No category with Id {id} was found.");
            }

            return categoryToEdit;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit(CategoryEditInputModel editInput)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please fill in all required fields.");
            }

            var editResult = await this.categoryService
                                       .Edit(editInput);

            if(editResult == null)
            {
                return StatusCode(505);
            }

            return StatusCode(200);
        }
    }
}
