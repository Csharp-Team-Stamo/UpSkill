namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UpSkill.Data.Models;
    using UpSkill.Infrastructure.Models.Coach;
    using UpSkill.Services.Data.Contracts;

    public class CoachController : AdminController
    {
        private readonly ICoachService coachService;

        public CoachController(ICoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CoachCreateInputModel input)
        {
           var coach = await this.coachService.Create(input);

            if(coach == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CoachCreateInputModel>>> All()
        {
            /* TODO create a CoachAdminListingServiceModel &
            * make it the type of the returned collection */

            var coaches = await this.coachService.GetAll();

            return new List<CoachCreateInputModel>(coaches);
        }

        [HttpPut("Edit/{id}")]
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

        [HttpDelete("Delete/{id}")]
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
