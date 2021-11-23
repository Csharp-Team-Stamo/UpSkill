namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
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

        [HttpGet("GetByIdAsync")]
        public async Task<CourseDescriptionModel> GetByIdAsync(int coachId)
        {
            return await coursesService.GetByIdAsync(coachId);
        }

        [HttpGet("GetAllByOwnerId")]
        public CoursesListingCatalogModel GetAllByOwnerId([FromQuery] string ownerId)
        {
            return coursesService.GetAllByOwnerId(ownerId);
        }

        [HttpGet("GetAll")]
        public CoursesListingCatalogModel GetAll([FromQuery] string ownerId)
        {
            return coursesService.GetAll(ownerId);
        }

        [HttpPost("AddCourseInOwnerCoursesCollectionAsync")]
        public async Task AddCourseInOwnerCoursesCollectionAsync([FromQuery] int courseId, [FromQuery] string ownerId)
        {
            await coursesService.AddCourseInOwnerCoursesCollectionAsync(courseId, ownerId);
        }

        [HttpDelete("RemoveCourseFromOwnerCoursesCollectionAsync")]
        public async Task<ActionResult> RemoveCourseFromOwnerCoursesCollectionAsync([FromQuery] int courseId, [FromQuery] string ownerId)
        {
            await coursesService.RemoveCourseFromOwnerCoursesCollectionAsync(courseId, ownerId);

            return Ok();
        }
    }
}
