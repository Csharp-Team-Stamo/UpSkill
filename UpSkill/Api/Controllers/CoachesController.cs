namespace UpSkill.Api.Controllers
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Infrastructure.Models.CoachDescriptionModal;
    using Infrastructure.Models.Coaches;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Services.Data.Contracts;
    using UpSkill.Infrastructure.Models.Coach.Sessions;
    using UpSkill.Services.Data;

    [Route("[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly ICoachesService coachesService;
        private readonly IOptions<CalendlyOptions> options;
        private readonly IHttpClientFactory clientFactory;

        public CoachesController(ICoachesService coachesService, IOptions<CalendlyOptions> options, IHttpClientFactory clientFactory)
        {
            this.coachesService = coachesService;
            this.options = options;
            this.clientFactory = clientFactory;
        }

        [HttpGet("GetAll")]
        public CoachesListingCatalogModel GetAll([FromQuery]string ownerId)
        {
            return coachesService.GetAll(ownerId);
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

        [HttpPost("AddCoachInOwnerCoachesCollectionAsync")]
        public async Task AddCoachInOwnerCoachesCollectionAsync([FromQuery]string coachId, [FromQuery] string ownerId)
        {
           await coachesService.AddCoachInOwnerCoachesCollectionAsync(coachId, ownerId);
        }

        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSession([FromBody] CoachSessionRequestModel input)
        {

            var eventUri = input.EventUri;
            var inviteeUri = input.InviteeUri;

            var request = new HttpRequestMessage(HttpMethod.Get,
            $"{eventUri}");
            request.Headers.Add("Authorization", $"Bearer {options.Value.Token}");

            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<CoachSessionEventResponseModel>(content);
                return StatusCode(200, "Hi from controller!");
            }

            return StatusCode((int)response.StatusCode, response.Content);
        }


        [HttpDelete("RemoveCoachFromOwnerCoachCollectionAsync")]
        public async Task<ActionResult> RemoveCoachFromOwnerCoachCollectionAsync([FromQuery]string coachId, [FromQuery] string ownerId)
        {
           await coachesService.RemoveCoachFromOwnerCoachCollectionAsync(coachId, ownerId);

           return Ok();
        }

    }
}
