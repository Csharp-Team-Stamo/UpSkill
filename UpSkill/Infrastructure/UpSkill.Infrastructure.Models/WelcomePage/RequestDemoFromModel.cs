namespace UpSkill.Infrastructure.Models.WelcomePage
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.VisualBasic;
    using static Common.GlobalConstants.WelcomePageConst;

    public class RequestDemoFromModel
    {
        [Required]
        [MinLength(NameMinLen)]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        [Required]
        [MinLength(CompanyNameMinLen)]
        [MaxLength(CompanyNameMaxLen)]
        public string CompanyName { get; set; }

        [Required]
        [RegularExpression(EmailRegEx)]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
