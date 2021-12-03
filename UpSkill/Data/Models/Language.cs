namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using static DataConstants.LanguageConstants;

    public class Language : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; init; }

        public ICollection<CoachLanguage> Coaches { get; set; } = 
            new HashSet<CoachLanguage>();

        public ICollection<Course> Courses { get; set; } = 
            new HashSet<Course>();
    }
}
