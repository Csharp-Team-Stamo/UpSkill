using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Category;

namespace UpSkill.Infrastructure.Models.Coach
{
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
