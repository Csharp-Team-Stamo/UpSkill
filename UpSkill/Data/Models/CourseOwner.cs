namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CourseOwner : BaseModel<int>
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
