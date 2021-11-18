namespace UpSkill.Infrastructure.Models.Course
{
    public class AdminCourseListingServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice => $"{this.Price:f0}";
        public string AuthorFullName { get; set; }
        public string AuthorCompanyLogo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
