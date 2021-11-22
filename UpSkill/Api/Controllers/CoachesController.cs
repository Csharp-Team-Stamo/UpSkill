namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.CoachDescriptionModal;
    using Infrastructure.Models.Coaches;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;
    using UpSkill.Infrastructure.Common.Pagination.Coaches;

    [Route("[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachService coachesService;

        public CoachesController(ICoachService coachesService)
        {
            this.coachesService = coachesService;
        }

        [HttpGet("GetAll")]
        public CoachesListingCatalogModel GetAll([FromQuery]string ownerId, [FromQuery] CoachesParameters parameters)
        {
            return coachesService.GetAll(ownerId, parameters);
        }

        [HttpGet("GetByIdAsync")]
        public async Task<CoachDescriptionModel> GetByIdAsync(string coachId)
        {
           return await coachesService.GetByIdAsync(coachId);
        }

        [HttpGet("GetAllByOwnerId")]
        public CoachesListingCatalogModel GetAllByOwnerId([FromQuery] string ownerId, [FromQuery] CoachesParameters parameters)
        {
            return coachesService.GetAllByOwnerId(ownerId, parameters);
        }

        [HttpPost("AddCoachInOwnerCoachesCollectionAsync")]
        public async Task AddCoachInOwnerCoachesCollectionAsync([FromQuery]string coachId, [FromQuery] string ownerId)
        {
           await coachesService.AddCoachInOwnerCoachesCollectionAsync(coachId, ownerId);
        }
        
        [HttpDelete("RemoveCoachFromOwnerCoachCollectionAsync")]
        public async Task<ActionResult> RemoveCoachFromOwnerCoachCollectionAsync([FromQuery]string coachId, [FromQuery] string ownerId)
        {
           await coachesService.RemoveCoachFromOwnerCoachCollectionAsync(coachId, ownerId);

           return Ok();
        }
    }
}
