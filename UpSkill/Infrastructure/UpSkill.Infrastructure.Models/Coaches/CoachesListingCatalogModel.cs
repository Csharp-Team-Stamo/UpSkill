namespace UpSkill.Infrastructure.Models.Coaches
{
    using System.Collections.Generic;

    public class CoachesListingCatalogModel
    {
        public ICollection<string> OwnerCoachCollectionIds { get; set; } = new List<string>();

        public ICollection<CoachInListCatalogModel> Coaches { get; set; } = new List<CoachInListCatalogModel>();
    }
}
