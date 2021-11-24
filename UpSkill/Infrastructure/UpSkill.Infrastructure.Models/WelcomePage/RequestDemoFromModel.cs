namespace UpSkill.Infrastructure.Models.WelcomePage
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Common.Attributes;

    using static Common.GlobalConstants.WelcomePageConst;

    public class RequestDemoFromModel
    {

        [Required]
        [MinLength(NameMinLen, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(NameMaxLen, ErrorMessage = "Name can be maximum 20 characters")]
        public string Name { get; set; }

        [DisplayName("Company Name")]
        [Required]
        [MinLength(CompanyNameMinLen, ErrorMessage = "Company Name must be at least 2 characters")]
        [MaxLength(CompanyNameMaxLen, ErrorMessage = "Company Name can be maximum 20 characters")]
        public string CompanyName { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
