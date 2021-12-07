namespace UpSkill.Infrastructure.Models.Language
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageCreateInputModel
    {
        private const string RequiredErrorMessage = " is required.";
        private const int NameMinLen = 2;
        private const int NameMaxLen = 20;

        [Required(ErrorMessage = nameof(Name) + RequiredErrorMessage)]
        [StringLength(NameMaxLen,
                      MinimumLength = NameMinLen,
                      ErrorMessage = "Name should be {1} to {0} symbols long.")]
        public string Name { get; set; }
    }
}
