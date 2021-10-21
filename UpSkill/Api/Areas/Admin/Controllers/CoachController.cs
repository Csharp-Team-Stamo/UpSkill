namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Data.Models;

    public class CoachController : AdminController
    {
        [HttpPost]
        public async Task<ActionResult> Create()
        {
            // TODO create CoachCreateInputModel & pass it [FromBody] to the ctor
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> ListAll()
        {
            /* TODO create a CoachAdminListingServiceModel &
            * make it the type of the returned collection */

            return new List<Coach>();
        }

        [HttpPut("id")]
        public async Task<ActionResult> Edit(string id)
        {
            /* TODO create CoachEditInputModel &
             * pass it [Fromody] to the ctor */

            if(id == null)
            {
                return BadRequest("A valid Id is needed.");
            }

            return StatusCode(200);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest("A valid Id is needed.");
            }

            return StatusCode(200);
        }
    }
}
