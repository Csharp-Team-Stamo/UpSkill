namespace UpSkill.Infrastructure.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseCreateInputModel
    {
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
        private const int SkillsMinLen = 25;
        private const int SkillsMaxLen = 150;

        [Required(ErrorMessage = nameof(CategoryId) + RequiredErrorMessage)]
        [Display(Name = "Category Id")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = nameof(Name) + RequiredErrorMessage)]
        [StringLength(NameMaxLen,
                      MinimumLength = NameMinLen,
                      ErrorMessage = "Name should be {1} to {0} symbols long.")]
        public string Name { get; set; }

        [Required(ErrorMessage = nameof(Description) + RequiredErrorMessage)]
        [StringLength(DescriptionMaxLen,
                      MinimumLength = DescriptionMinLen,
                      ErrorMessage = "Description should be {1} to {0} symbols long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = nameof(ImageUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name="Image URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = nameof(AuthorFullName) + RequiredErrorMessage)]
        [StringLength(AuthorNameMaxLen,
                      MinimumLength = AuthorNameMinLen,
                      ErrorMessage = "Author name should be {1} to {0} symbols long.")]
        [Display(Name = "Author Full Name")]
        public string AuthorFullName { get; set; }

        [Required(ErrorMessage = nameof(AuthorImageUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name="Author Image URL")]
        public string AuthorImageUrl { get; set; }

        [Required(ErrorMessage = nameof(CompanyLogoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Author Company Logo URL")]
        public string CompanyLogoUrl { get; set; }

        [Required(ErrorMessage = nameof(CompanyName) + RequiredErrorMessage)]
        [StringLength(AuthorCompanyNameMaxLen,
                      MinimumLength = AuthorCompanyNameMinLen,
                      ErrorMessage = "Author company name should be {1} to {0} symbols long.")]
        [Display(Name = "Author Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = nameof(DurationInHours) + RequiredErrorMessage)]
        [StringLength(DurationMaxLen,
                      MinimumLength = DurationMinLen,
                      ErrorMessage = "Duration should be {1} to {0} symbols long.")]
        [Display(Name = "Duration In Hours")]
        public string DurationInHours { get; set; }

        [Required(ErrorMessage = nameof(LecturesCount) + RequiredErrorMessage)]
        [StringLength(LecturesNumMaxLen,
            MinimumLength = LectureNumMinLen,
            ErrorMessage = "Count should be {1} to {0} symbols long.")]
        [Display(Name = "Lectures Count")]
        public string LecturesCount { get; set; }

        [Required(ErrorMessage = nameof(SkillsLearn) + RequiredErrorMessage)]
        [StringLength(SkillsMaxLen,
            MinimumLength = SkillsMinLen,
            ErrorMessage = "Skills should be {0} to {1} symbols long, separated by #.")]
        [Display(Name = "Skills To Learn")]
        public string SkillsLearn { get; set; }

        [Range(typeof(decimal), "0.00", "9999.00")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = nameof(LanguageId) + RequiredErrorMessage)]
        [Display(Name = "Language Id")]
        public int LanguageId { get; set; }

        [Required(ErrorMessage = nameof(VideoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
