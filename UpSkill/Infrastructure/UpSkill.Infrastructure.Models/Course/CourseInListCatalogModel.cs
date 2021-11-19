namespace UpSkill.Infrastructure.Models.Course
{
    using System.Collections.Generic;

    public class CourseInListCatalogModel
    {
        public int Id { get; set; }

        public string AuthorFullName { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string CompanyName { get; set; }

        public string CompanyLogoUrl { get; set; }

        public decimal PricePerPerson { get; set; }

        public string LanguageName { get; set; }
    }
}
