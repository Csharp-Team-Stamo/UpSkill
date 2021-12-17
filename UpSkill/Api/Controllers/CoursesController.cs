namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Course;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using Infrastructure.Models.Lecture;

    [Route("[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService coursesService;
        private readonly ILectureService lectureService;

        public CoursesController(
            ICoursesService coursesService,
            ILectureService lectureService)
        {
            this.coursesService = coursesService;
            this.lectureService = lectureService;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<CourseDescriptionModel> GetByIdAsync(int coachId)
        {
            return await coursesService.GetByIdAsync(coachId);
        }

        [HttpGet("GetAllByOwnerId")]
        public async Task<CoursesListingCatalogModel> GetAllByOwnerId([FromQuery] string ownerId)
        {
            return await coursesService.GetAllByOwnerId(ownerId);
        }

        [HttpGet("GetAll")]
        public async Task<CoursesListingCatalogModel> GetAll([FromQuery] string ownerId)
        {
            return await coursesService.GetAll(ownerId);
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

        [HttpGet("GetLectureById/{courseId}/{lectureId}")]
        public async Task<ActionResult<LectureDetailsModel>> GetLectureById(
            int courseId, int lectureId)
        {
            if (courseId < 1 || lectureId < 1)
            {
                return BadRequest("Both valid Course Id and Lecture Id are required.");
            }

            var lecture = await this.lectureService
                .GetLectureById(courseId, lectureId);

            if (lecture == null)
            {
                return NotFound(
                    "Either the lecture, or at least one of the Ids you provided, does not exist.");
            }

            return lecture;
        }
    }
}
