namespace UpSkill.Infrastructure.Models.CourseVote
{
    using System.ComponentModel.DataAnnotations;

    public class CourseVoteCreateInputModel
    {
        private const string RequiredErrorMessage = " is required.";

        [Required(ErrorMessage = nameof(CourseId) + RequiredErrorMessage)]
        [Display(Name = "Course Id")]
        public int CourseId { get; init; }

        [Required(ErrorMessage = nameof(UserId) + RequiredErrorMessage)]
        [Display(Name = "User Id")]
        public string UserId { get; init; }

        [Required(ErrorMessage = nameof(Value) + RequiredErrorMessage)]
        [Range(1, 6)]
        public byte Value { get; init; }
    }
}
