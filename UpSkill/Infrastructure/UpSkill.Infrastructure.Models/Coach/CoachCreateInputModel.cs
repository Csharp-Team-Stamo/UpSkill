namespace UpSkill.Infrastructure.Models.Coach
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Models.Category;

    public class CoachCreateInputModel
    {
        public string Id { get; set; }
        public CategoryCreateInputModel Category { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Company { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }
    }
}
