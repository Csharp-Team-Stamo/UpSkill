namespace UpSkill.Infrastructure.Models.Language
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
