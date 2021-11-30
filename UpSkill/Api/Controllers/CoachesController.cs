﻿namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coach;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Services.Data.Contracts;
    using UpSkill.Services.Data;

    [Route("[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachesService coachesService;

        public CoachesController(ICoachesService coachesService)
        {
            this.coachesService = coachesService;
        }

        [HttpGet("GetByIdAsync")]
        public async Task<CoachDescriptionModel> GetByIdAsync(string coachId)
        {
            return await coachesService.GetByIdAsync(coachId);
        }

        [HttpGet("GetAllByOwnerId")]
        public CoachesListingCatalogModel GetAllByOwnerId([FromQuery] string ownerId)
        {
            return coachesService.GetAllByOwnerId(ownerId);
        }

        [HttpGet("GetAllByEmployeeId")]
        public CoachesListingCatalogModel GetAllByEmployeeId([FromQuery] string ownerId, [FromQuery] string userId)
        {
            return coachesService.GetAllByEmployeeId(ownerId, userId);
        }

        [HttpGet("GetAll")]
        public CoachesListingCatalogModel GetAll([FromQuery] string ownerId)
        {
            return coachesService.GetAll(ownerId);
        }

        [HttpPost("AddCoachInOwnerCoachesCollectionAsync")]
        public async Task AddCoachInOwnerCoachesCollectionAsync([FromQuery] string coachId, [FromQuery] string ownerId)
        {
            await coachesService.AddCoachInOwnerCoachesCollectionAsync(coachId, ownerId);
        }

        [HttpDelete("RemoveCoachFromOwnerCoachCollectionAsync")]
        public async Task<ActionResult> RemoveCoachFromOwnerCoachCollectionAsync([FromQuery] string coachId, [FromQuery] string ownerId)
        {
            await coachesService.RemoveCoachFromOwnerCoachCollectionAsync(coachId, ownerId);

            return Ok();
        }
    }
}
