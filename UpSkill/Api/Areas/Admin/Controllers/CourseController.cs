namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Infrastructure.Models.Course;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    public class CourseController : AdminController
    {
        private readonly IAdminCourseService courseService;

        public CourseController(
            IAdminCourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] CourseCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Please fill in all required fields.");
            }

            var createResult = await this.courseService.Create(input);

            if(createResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AdminCourseListingServiceModel>>> All()
        {
            var allCourses = await this.courseService.All();

            if(allCourses.Any() == false)
            {
                return NoContent();
            }

            return new List<AdminCourseListingServiceModel>(allCourses);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<CourseDetailsServiceModel>> Details(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }

            var course = await this.courseService.GetCourse(id);

            return course;
        }


        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromBody] CourseEditInputModel input)
        {
            // TODO create a CourseEditInputModel & pass it [FromBody] to the ctor

            if(input == null || 
                ModelState.IsValid == false || 
                input.Id <= 0)
            {
                return BadRequest("A valid Id is needed.");
            }

            var editResult = await this.courseService.Edit(input);

            if(editResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("A valid Id is needed.");
            }

            var deleteResult = await this.courseService.Delete(id);

            if(deleteResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }
    }
}
