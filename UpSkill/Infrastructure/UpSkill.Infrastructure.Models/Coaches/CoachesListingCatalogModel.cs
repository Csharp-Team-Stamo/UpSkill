namespace UpSkill.Infrastructure.Models.Coaches
{
    using System.Collections.Generic;
    using UpSkill.Infrastructure.Common.Pagination;

    public class CoachesListingCatalogModel
    {
        public ICollection<string> OwnerCoachCollectionIds { get; set; } = new List<string>();

        public VirtualizeResponse<CoachInListCatalogModel> Coaches { get; set; } = new ();
    }
}
