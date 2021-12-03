namespace UpSkill.Infrastructure.Models.Coach
{
    using System.ComponentModel.DataAnnotations;
    using Common.Attributes;

    public class CoachCreateInputModel
    {
        public string Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string AvatarImgUrl { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0.00", "1000.00")]
        public decimal PricePerSession { get; set; }

        [Required]
        public string VideoUrl { get; set; }

        [Required]
        public string CalendlyPopupUrl { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string SessionDescription { get; set; }

        [Required]
        [MaxLength(150)]
        public string SkillsLearn { get; set; }

        [Required]
        public string DiscussionDurationInMin { get; set; }

        [Required]
        public string ResourcesCount { get; set; }
    }
}
