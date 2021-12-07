namespace UpSkill.Infrastructure.Models.Company
{
    using System.ComponentModel.DataAnnotations;
    using Common.Attributes;

    public class CompanyCreateInputModel
    {
        private const string RequiredErrorMessage = " is required.";
        private const int NameMinLen = 2;
        private const int NameMaxLen = 30;

        [Required(ErrorMessage = nameof(Name) + RequiredErrorMessage)]
        [MinLength(NameMinLen)]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        public string UIC { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = nameof(Email) + RequiredErrorMessage)]
        [IsEmail]
        public string Email { get; set; }
    }
}
