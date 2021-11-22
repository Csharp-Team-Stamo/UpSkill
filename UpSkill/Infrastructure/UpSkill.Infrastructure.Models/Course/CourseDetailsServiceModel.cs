namespace UpSkill.Infrastructure.Models.Course
{
    using Category;

    public class CourseDetailsServiceModel
    {
        public int Id { get; set; }

        public CategoryDetailsServiceModel Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorFullName { get; set; }
        public string CompanyLogoUrl { get; set; }
        public decimal Price { get; set; }
        public string VideoUrl { get; set; }
    }
}
