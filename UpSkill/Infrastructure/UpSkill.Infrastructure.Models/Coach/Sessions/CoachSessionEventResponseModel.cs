
namespace UpSkill.Infrastructure.Models.Coach.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;

    public class CoachSessionEventResponseModel
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("event_memberships")]
        public List<EventMembership> EventMemberships { get; set; }
    }

    public class EventMembership
    {
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
