namespace UpSkill.Infrastructure.Models.Coach.Sessions
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Newtonsoft.Json;

    public class CoachSessionInviteeResponseModel
    {


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
            [JsonProperty("cancel_url")]
            public string CancelUrl { get; set; }

            [JsonProperty("created_at")]
            public DateTime CreatedAt { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("event")]
            public string Event { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("new_invitee")]
            public object NewInvitee { get; set; }

            [JsonProperty("old_invitee")]
            public object OldInvitee { get; set; }

            [JsonProperty("questions_and_answers")]
            public List<object> QuestionsAndAnswers { get; set; }

            [JsonProperty("reschedule_url")]
            public string RescheduleUrl { get; set; }

            [JsonProperty("rescheduled")]
            public bool Rescheduled { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("text_reminder_number")]
            public object TextReminderNumber { get; set; }

            [JsonProperty("timezone")]
            public string Timezone { get; set; }

            [JsonProperty("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [JsonProperty("uri")]
            public string Uri { get; set; }

            [JsonProperty("canceled")]
            public bool Canceled { get; set; }

            //[JsonProperty("payment")]
            //public Payment Payment { get; set; }

    }

        //public class Payment
        //{
        //    [JsonProperty("external_id")]
        //    public string ExternalId { get; set; }

        //    [JsonProperty("provider")]
        //    public string Provider { get; set; }

        //    [JsonProperty("amount")]
        //    public double Amount { get; set; }

        //    [JsonProperty("currency")]
        //    public string Currency { get; set; }

        //    [JsonProperty("terms")]
        //    public string Terms { get; set; }

        //    [JsonProperty("successful")]
        //    public bool Successful { get; set; }
        //}

}
