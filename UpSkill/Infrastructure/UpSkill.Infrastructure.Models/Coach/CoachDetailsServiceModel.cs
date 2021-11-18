﻿using UpSkill.Infrastructure.Models.Category;

namespace UpSkill.Infrastructure.Models.Coach
{
    public class CoachDetailsServiceModel
    {
        public string Id { get; set; }
        public CategoryDetailsServiceModel Category { get; set; }

        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public string Company { get; set; }
        public decimal PricePerSession { get; set; }
    }
}
