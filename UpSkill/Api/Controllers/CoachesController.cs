namespace UpSkill.Api.Controllers
{
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
        public CoachesListingCatalogModel GetAll()
        {
            return coachesService.GetAll();
        }
    }
}
