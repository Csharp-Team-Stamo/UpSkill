namespace Data.Models
{
    using UpSkill.Data.Common.Models;
    using UpSkill.Data.Models;

    public class CoachLanguage : BaseDeletableModel<int>
    {
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
