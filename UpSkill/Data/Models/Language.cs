namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public string Name { get; init; }

        public ICollection<CoachLanguage> Coaches { get; set; } = new HashSet<CoachLanguage>();

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
