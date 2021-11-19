namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using UpSkill.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public ICollection<LectureResource> Resources { get; set; } =
            new List<LectureResource>();
    }
}
