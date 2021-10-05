namespace UpSkill.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class SessionSlot : BaseDeletableModel<int>
    {
        [Required]
        public DateTime Start { get; init; }

        [Required]
        public DateTime End { get; init; }

        [Required]
        public string Description { get; init; }
    }
}
