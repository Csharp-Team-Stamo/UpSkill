namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;

    public class LectureResource : BaseDeletableModel<int>
    {
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public byte[] Attachment { get; set; }
    }
}
