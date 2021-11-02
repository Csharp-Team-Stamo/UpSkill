namespace UpSkill.Api.Controllers
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coaches;
    using Microsoft.AspNetCore.Mvc;
    using Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachesService coachesService;

        public CoachesController(ICoachesService coachesService)
        {
            this.coachesService = coachesService;
        }

        [HttpGet("GetAll")]
        public CoachesListingCatalogModel GetAll([FromQuery]string userId)
        {
            return coachesService.GetAll(userId);
        }

        [HttpGet("GetAllByOwnerId")]
        public CoachesListingCatalogModel GetAllByOwnerId([FromQuery] string userId)
        {
            return coachesService.GetAllByOwnerId(userId);
        }

        [HttpPost("AddCoachInOwnerCoachesCollectionAsync")]
        public async Task AddCoachInOwnerCoachesCollectionAsync([FromQuery]string coachId, [FromQuery] string ownerId)
        {
           await coachesService.AddCoachInOwnerCoachesCollectionAsync(coachId, ownerId);
        }
        
        [HttpDelete("RemoveCoachFromOwnerCoachCollection")]
        public async Task<ActionResult> RemoveCoachFromOwnerCoachCollectionAsync([FromQuery]string coachId, [FromQuery] string userId)
        {
           await coachesService.RemoveCoachFromOwnerCoachCollectionAsync(coachId, userId);

           return Ok();
        }

    }
}
