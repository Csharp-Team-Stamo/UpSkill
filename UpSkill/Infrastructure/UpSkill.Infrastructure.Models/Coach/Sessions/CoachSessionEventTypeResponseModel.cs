namespace UpSkill.Infrastructure.Models.Coach.Sessions
{
    using System;
    using Newtonsoft.Json;
    public class CoachSessionEventTypeResponseModel
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("scheduling_url")]
        public string SchedulingUrl { get; set; }

        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
