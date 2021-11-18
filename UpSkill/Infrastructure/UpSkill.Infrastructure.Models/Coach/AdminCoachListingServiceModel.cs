namespace UpSkill.Infrastructure.Models.Coach
{
    public class AdminCoachListingServiceModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }

        public string AvatarUrl { get; set; }
        public decimal Price { get; set; }
        public string FormattedPrice => $"{this.Price:f0}";
        public string CompanyLogoUrl { get; set; }
        public string CategoryName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
