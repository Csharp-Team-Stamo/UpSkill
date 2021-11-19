namespace UpSkill.Api.Controllers
{
    using Infrastructure.Models.Course;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet("GetAll")]
        public CoursesListingCatalogModel GetAll([FromQuery] string ownerId)
        {
            return coursesService.GetAll(ownerId);
        }
    }
}
