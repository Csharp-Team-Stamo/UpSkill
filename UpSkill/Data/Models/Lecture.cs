namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static DataConstants.LectureConstants;

    public class Lecture : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [Required]
        [MaxLength(TitleMaxLen)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        public ICollection<LectureResource> Resources { get; set; } =
            new List<LectureResource>();
    }
}
