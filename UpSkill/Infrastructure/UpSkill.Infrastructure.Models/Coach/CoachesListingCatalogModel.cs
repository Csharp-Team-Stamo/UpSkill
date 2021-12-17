namespace UpSkill.Infrastructure.Models.Coach
{
    using System.Collections.Generic;

    public class CoachesListingCatalogModel
    {
        public ICollection<string> OwnerCoachCollectionIds { get; set; } = new List<string>();

        public List<CoachInListCatalogModel> Coaches { get; set; } = new List<CoachInListCatalogModel>();

        public ICollection<EmployeeCoachSessions> EmployeeCoachSessions = new HashSet<EmployeeCoachSessions>();
    }
}
