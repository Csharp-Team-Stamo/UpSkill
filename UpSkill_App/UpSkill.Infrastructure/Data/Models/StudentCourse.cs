namespace UpSkill.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class StudentCourse
    {
        [Required]
        public string StudentId { get; set; }
        public Employee Student { get; set; }

        [Required]
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
