namespace UpSkill.Infrastructure.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseEditInputModel
    {
        public int Id { get; set; }
        private const string RequiredErrorMessage = " is required.";
        private const int NameMinLen = 5;
        private const int NameMaxLen = 40;
        private const int DescriptionMinLen = 10;
        private const int DescriptionMaxLen = 150;
        private const int AuthorNameMinLen = 2;
        private const int AuthorNameMaxLen = 30;
        private const int AuthorCompanyNameMinLen = 2;
        private const int AuthorCompanyNameMaxLen = 30;
        private const int DurationMinLen = 1;
        private const int DurationMaxLen = 3;
        private const int LectureNumMinLen = 1;
        private const int LecturesNumMaxLen = 3;
        private const int SkillsMinLen = 10;
        private const int SkillsMaxLen = 150;

        [Required]
        [Range(1, 100, ErrorMessage = "Category" + RequiredErrorMessage)]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Required(ErrorMessage = nameof(Name) + RequiredErrorMessage)]
        [MinLength(NameMinLen)]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        [Required(ErrorMessage = nameof(Description) + RequiredErrorMessage)]
        [MinLength(DescriptionMinLen)]
        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required(ErrorMessage = nameof(ImageUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = nameof(AuthorFullName) + RequiredErrorMessage)]
        [MinLength(AuthorNameMinLen)]
        [MaxLength(AuthorNameMaxLen)]
        [Display(Name = "Author Full Name")]
        public string AuthorFullName { get; set; }

        [Required(ErrorMessage = nameof(AuthorImageUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Author Image URL")]
        public string AuthorImageUrl { get; set; }

        [Required(ErrorMessage = nameof(CompanyLogoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Author Company Logo URL")]
        public string CompanyLogoUrl { get; set; }

        [Required(ErrorMessage = nameof(CompanyName) + RequiredErrorMessage)]
        [MinLength(AuthorCompanyNameMinLen)]
        [MaxLength(AuthorCompanyNameMaxLen)]
        [Display(Name = "Author Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = nameof(DurationInHours) + RequiredErrorMessage)]
        [MinLength(DurationMinLen)]
        [MaxLength(DurationMaxLen)]
        [Display(Name = "Duration In Hours")]
        public string DurationInHours { get; set; }

        [Required(ErrorMessage = nameof(LecturesCount) + RequiredErrorMessage)]
        [MinLength(LectureNumMinLen)]
        [MaxLength(LecturesNumMaxLen)]
        [Display(Name = "Lectures Count")]
        public string LecturesCount { get; set; }

        [Required(ErrorMessage = nameof(SkillsLearn) + RequiredErrorMessage)]
        [MinLength(SkillsMinLen)]
        [MaxLength(SkillsMaxLen)]
        [Display(Name = "Skills To Learn")]
        public string SkillsLearn { get; set; }

        [Range(typeof(decimal), "0", "9999")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Language" + RequiredErrorMessage)]
        [Display(Name = "Language Id")]
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        [Required(ErrorMessage = nameof(VideoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
