namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Data.Models;

    public class CourseController : AdminController
    {
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            // TODO create a CourseCreateInputModel & pass it [FromBody] to the ctor
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> ListAll()
        {
            // TODO create CourseAdminListingSM & return a collection
            return new List<Course>();
        }

        [HttpPost("id")]
        public async Task<ActionResult> Edit(int id)
        {
            // TODO create a CourseEditInputModel & pass it [FromBody] to the ctor

            if(id <= 0)
            {
                return BadRequest("A valid Id is needed.");
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

            return StatusCode(200);
        }
    }
}
