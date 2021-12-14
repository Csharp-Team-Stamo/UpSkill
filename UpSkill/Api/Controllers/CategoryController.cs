namespace UpSkill.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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

        [HttpGet("GetAllNamesAsync")]
        public async Task<ICollection<string>> GetAllNamesAsync()
        {
            return await categoriesService.GetAllNamesAsync();
        }
    }
}
