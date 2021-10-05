namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class Grade : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string StudentId { get; set; }
        public Employee Student { get; set; }

        [Required]
        public double Value { get; set; }
    }
}
