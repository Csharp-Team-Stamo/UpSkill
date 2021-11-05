namespace UpSkill.Data.Models
{
    using Common.Models;

    public class CoachOwner
    {
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
