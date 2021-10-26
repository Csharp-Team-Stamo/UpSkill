namespace UpSkill.Infrastructure.Models.Coach
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Category;

    public class CoachEditInputModel
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public CategoryEditInputModel Category { get; set; }

        [Required]
        public string Company { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }
    }
}
