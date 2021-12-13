namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models.Coach;
    using Services.Data.Admin.Contracts;
    using System.Linq;

    public class CoachController : AdminController
    {
        private readonly IAdminCoachService coachService;

        public CoachController(IAdminCoachService coachService)
        {
            this.coachService = coachService;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody]CoachCreateInputModel input)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest("Valid coach data is required.");
            }
            
            var coach = await this.coachService.Create(input);

            if(coach == null)
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<AdminCoachListingServiceModel>>> All()
        {
            var coaches = await this.coachService.GetAll();

            if(coaches.Any() == false)
            {
                return NotFound();
            }

            return new List<AdminCoachListingServiceModel>(coaches);
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<CoachDetailsServiceModel>> Details(string id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var coachInDb = await this.coachService.GetCoachDetails(id);

            if(coachInDb == null)
            {
                return NotFound();
            }

            return coachInDb;
        }

        [HttpGet("Edit/{id}")]
        public async Task<ActionResult<CoachEditInputModel>> Edit(string id)
        {
            if(id == null)
            {
                return BadRequest("A valid Id is required.");
            }

            var editInput = await this.coachService.GetCoachEditModel(id);

            if (editInput == null)
            {
                return NotFound($"Coach with Id {id} was not found.");
            }

            return editInput;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromBody]CoachEditInputModel editInput)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var editResult = await this.coachService
                .ExecuteEdit(editInput);

            if(editResult == null)
            {
                return StatusCode(500);
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

            var deleteResult = await this.coachService.Delete(id);

            if (deleteResult == null)
            {
                return StatusCode(500);
            }

            return StatusCode(200);
        }
    }
}
