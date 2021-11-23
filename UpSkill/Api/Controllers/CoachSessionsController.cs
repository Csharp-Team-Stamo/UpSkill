namespace UpSkill.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using UpSkill.Infrastructure.Models.Coach.Sessions;
    using UpSkill.Services.Data;
    using UpSkill.Services.Data.Contracts;

    [Route("[controller]")]
    [ApiController]
    public class CoachSessionsController : ControllerBase
    {
        private readonly IOptions<CalendlyOptions> options;
        private readonly IHttpClientFactory clientFactory;
        private readonly ICoachSessionsService coachSessionsService;

        public CoachSessionsController(
            IOptions<CalendlyOptions> options,
            IHttpClientFactory clientFactory,
            ICoachSessionsService coachSessionsService
            )
        {
            this.options = options;
            this.clientFactory = clientFactory;
            this.coachSessionsService = coachSessionsService;
        }

        [HttpPost("AddSession")]
        public async Task<IActionResult> AddSession([FromBody] CoachSessionRequestModel input)
        {

            var eventUri = input.EventUri;
            var inviteeUri = input.InviteeUri;

            CoachSessionEventResponseModel eventPayload = null;
            CoachSessionInviteeResponseModel inviteePayload = null;
            string coachSchedulingUri = string.Empty;

            var response = await GetCalendlyPayload(eventUri);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var contentToDeserialize = JObject.Parse(content)["resource"].ToString();
                eventPayload = JsonConvert.DeserializeObject<CoachSessionEventResponseModel>(contentToDeserialize);
            }

            response = await GetCalendlyPayload(inviteeUri);

            if (response.IsSuccessStatusCode)
            {
                var contentAsString = response.Content.ReadAsStringAsync().Result;
                var contentAsJsonString = JObject.Parse(contentAsString)["resource"].ToString();
                inviteePayload = JsonConvert.DeserializeObject<CoachSessionInviteeResponseModel>(contentAsJsonString);
            }

            response = await GetCalendlyPayload(eventPayload.EventType);

            if (response.IsSuccessStatusCode)
            {
                var contentAsString = response.Content.ReadAsStringAsync().Result;
                var contentAsJsonString = JObject.Parse(contentAsString)["resource"].ToString();
                coachSchedulingUri = JsonConvert.DeserializeObject<CoachSessionEventTypeResponseModel>(contentAsJsonString).SchedulingUrl;
            }

            await coachSessionsService.AddSession(eventPayload, inviteePayload, coachSchedulingUri);

            return StatusCode((int)response.StatusCode, response.Content);
        }

        private async Task<HttpResponseMessage> GetCalendlyPayload(string eventUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, eventUri);
            request.Headers.Add("Authorization", $"Bearer {options.Value.Token}");
            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return response;
        }
    }
}
