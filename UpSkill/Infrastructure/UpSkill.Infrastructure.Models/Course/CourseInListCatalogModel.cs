namespace UpSkill.Infrastructure.Models.Course
{
    public class CourseInListCatalogModel
    {
        public int Id { get; set; }

        public string AuthorFullName { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public string CompanyLogoUrl { get; set; }

        public decimal PricePerPerson { get; set; }

        public string LanguageName { get; set; }
    }
}
