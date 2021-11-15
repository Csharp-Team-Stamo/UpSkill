namespace UpSkill.Infrastructure.Models.Coach
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CoachCreateInputModel
    {
        public string Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string AvatarImgUrl { get; set; }

        public string Email { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }

        [Required]
        public string CalendlyPopupUrl { get; set; }

        public int LanguageId { get; set; }

        public string SessionDescription { get; set; }

        public string SkillsLearn { get; set; }
        public string DiscussionDurationInMin { get; set; }
        public string ResourcesCount { get; set; }

        //public ICollection<int> Languages { get; set; } =
        //    new List<int>();
    }
}
