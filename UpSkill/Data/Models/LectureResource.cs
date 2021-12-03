namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static DataConstants.LectureResourceConstants;
    public class LectureResource : BaseDeletableModel<int>
    {
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        [Required]
        [MaxLength(TitleMaxLen)]
        public string Title { get; set; }


        public string Description { get; set; }

        [Required]
        public string DownloadUrl { get; set; }
    }
}
