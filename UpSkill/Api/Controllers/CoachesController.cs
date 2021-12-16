namespace UpSkill.Api.Controllers
{
    using System.Net.Http;
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

        [HttpGet("GetAllByOwnerIdAsync")]
        public async Task<CoachesListingCatalogModel> GetAllByOwnerIdAsync([FromQuery] string ownerId)
        {
            return await coachesService.GetAllByOwnerIdAsync(ownerId);
        }

        [HttpGet("GetAllByEmployeeIdAsync")]
        public async Task<CoachesListingCatalogModel> GetAllByEmployeeIdAsync([FromQuery] string ownerId, [FromQuery] string userId)
        {
            return await coachesService.GetAllByEmployeeIdAsync(ownerId, userId);
        }

        [HttpGet("GetAllAsync")]
        public async Task<CoachesListingCatalogModel> GetAllAsync([FromQuery] string ownerId)
        {
            return await coachesService.GetAllAsync(ownerId);
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
