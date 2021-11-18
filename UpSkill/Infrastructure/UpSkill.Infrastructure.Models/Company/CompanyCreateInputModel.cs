namespace UpSkill.Infrastructure.Models.Company
{
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Common.Attributes;

    public class CompanyCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        // [StringLength(UIC_Length)]
        public string UIC { get; set; }

        public string Address { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }
    }
}
