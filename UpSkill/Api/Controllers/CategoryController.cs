namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet("GetAllNames")]
        public ICollection<string> GetAllNames()
        {
            return categoriesService.GetAllNames();
        }
    }
}
