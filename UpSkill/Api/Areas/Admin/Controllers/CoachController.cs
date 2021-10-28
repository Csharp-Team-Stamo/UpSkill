﻿namespace UpSkill.Api.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Infrastructure.Models.Coach;
    using Services.Data.Contracts;
    using UpSkill.Data.Models;

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

            var coachInDb = await this.coachService.GetCoach(id);

            if(coachInDb == null)
            {
                return NotFound($"Coach with Id {id} was not found.");
            }

            var editInput = this.ConvertToEditModel(coachInDb);

            return editInput;
        }

        [HttpPut("Edit")]
        public async Task<ActionResult> Edit([FromBody]CoachEditInputModel editInput)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var editResult = await this.coachService.Edit(editInput);

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

        private CoachEditInputModel ConvertToEditModel(Coach coachInDb)
            => new()
            {
                Id = coachInDb.Id,
                Company = coachInDb.Company,
                FullName = coachInDb.FullName,
                PricePerSession = coachInDb.PricePerSession,
                CategoryId = coachInDb.Category.Id,
                CategoryName = coachInDb.Category.Name
            };
    }
}
