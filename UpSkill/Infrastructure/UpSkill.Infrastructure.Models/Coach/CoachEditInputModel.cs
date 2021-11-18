namespace UpSkill.Infrastructure.Models.Coach
{
    using System.ComponentModel.DataAnnotations;

    public class CoachEditInputModel
    {
        public string Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Required]
        public string Company { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }
    }
}
