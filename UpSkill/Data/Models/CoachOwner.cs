namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CoachOwner : BaseModel<int>
    {
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
