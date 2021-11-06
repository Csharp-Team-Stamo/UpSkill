﻿namespace UpSkill.Infrastructure.Models.Coach
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Models.Category;

    public class CoachCreateInputModel
    {
        public string Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.00, 1000.00)]
        public decimal PricePerSession { get; set; }
    }
}