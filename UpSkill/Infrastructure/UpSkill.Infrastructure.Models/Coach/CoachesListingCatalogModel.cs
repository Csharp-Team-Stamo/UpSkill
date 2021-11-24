namespace UpSkill.Infrastructure.Models.Coach
{
    using System.Collections.Generic;

    public class CoachesListingCatalogModel
    {
        public string OwnerId { get; set; }

        public ICollection<string> OwnerCoachCollectionIds { get; set; } = new List<string>();

        public ICollection<CoachInListCatalogModel> Coaches { get; set; } = 
            new List<CoachInListCatalogModel>();
    }
}
