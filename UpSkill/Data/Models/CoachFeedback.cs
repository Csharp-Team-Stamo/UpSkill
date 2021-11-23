namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using Common.Models;

    public class CoachFeedback : BaseDeletableModel<int>
    {
        [Required]
        public int Value { get; init; }

        public string Description { get; init; }
    }
}
