namespace UpSkill.Infrastructure.Models.Coach
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Models.Category;
    using UpSkill.Infrastructure.Models.Language;

    public class CoachCreateInputModel
    {
        public string Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string Email { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }

        public ICollection<int> Languages { get; set; } =
            new List<int>();
    }
}
