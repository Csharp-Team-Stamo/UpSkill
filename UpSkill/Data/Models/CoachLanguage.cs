namespace UpSkill.Data.Models
{
    using Common.Models;

    public class CoachLanguage
    {
        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
