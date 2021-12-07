﻿namespace UpSkill.Infrastructure.Models.Coach
{
    using System.ComponentModel.DataAnnotations;
    using Common.Attributes;

    public class CoachCreateInputModel
    {
        private const string RequiredErrorMessage = " is required";
        private const int CompanyNameMinLen = 2;
        private const int CompanyNameMaxLen = 30;
        private const int SessionDescriptionMinLen = 25;
        private const int SessionDescriptionMaxLen = 150;
        private const int SessionSkillsMinLen = 25;
        private const int SessionSkillsMaxLen = 150;

        [Required(ErrorMessage = nameof(CategoryId) + RequiredErrorMessage)]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = nameof(FullName) + RequiredErrorMessage)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = nameof(AvatarImgUrl) + RequiredErrorMessage)]
        [Display(Name = "Avatar Url")]
        public string AvatarImgUrl { get; set; }

        [Required(ErrorMessage = nameof(Email) + RequiredErrorMessage)]
        [IsEmail]
        public string Email { get; set; }

        [Required(ErrorMessage = nameof(CompanyName) + RequiredErrorMessage)]
        [StringLength(CompanyNameMaxLen, 
                      MinimumLength = CompanyNameMinLen, 
                      ErrorMessage = "Company Name should be {1} to {0} symbols long.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = nameof(CompanyLogoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Company Logo URL")]
        public string CompanyLogoUrl { get; set; }

        [DataType(DataType.Currency)]
        [Range(typeof(decimal), "0.00", "9999.00")]
        public decimal PricePerSession { get; set; }

        [Required(ErrorMessage = nameof(VideoUrl) + RequiredErrorMessage)]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }

        [Required(ErrorMessage = nameof(CalendlyPopupUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Calendly URL")]
        public string CalendlyPopupUrl { get; set; }

        [Required(ErrorMessage = nameof(LanguageId) + RequiredErrorMessage)]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = nameof(SessionDescription) + RequiredErrorMessage)]
        [StringLength(SessionDescriptionMaxLen,
                      MinimumLength = SessionDescriptionMinLen,
                      ErrorMessage = "Description sould be {1} to {0} symbols long.")]
        [Display(Name = "Session Description")]
        public string SessionDescription { get; set; }

        [Required(ErrorMessage = "Skills are required")]
        [StringLength(SessionSkillsMaxLen,
                      MinimumLength = SessionSkillsMinLen,
                      ErrorMessage = "Skills should be {1} to {0} symbols long.")]
        public string SkillsLearn { get; set; }

        [Required(ErrorMessage = nameof(DiscussionDurationInMin) + RequiredErrorMessage)]
        public string DiscussionDurationInMin { get; set; }

        [Required(ErrorMessage = nameof(ResourcesCount) + RequiredErrorMessage)]
        public string ResourcesCount { get; set; }
    }
}
