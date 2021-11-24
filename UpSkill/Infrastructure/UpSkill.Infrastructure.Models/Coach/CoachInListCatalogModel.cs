namespace UpSkill.Infrastructure.Models.Coach
{
    using System.Collections.Generic;

    public class CoachInListCatalogModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string Company { get; set; }

        public string CompanyLogoUrl { get; set; }

        public string CalendlyUrl { get; set; }

        public decimal PricePerSession { get; set; }

        public ICollection<string> Languages { get; set; }
    }
}
