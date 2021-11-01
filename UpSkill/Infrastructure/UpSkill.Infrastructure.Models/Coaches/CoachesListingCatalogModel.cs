namespace UpSkill.Infrastructure.Models.Coaches
{
    using System.Collections;
    using System.Collections.Generic;

    public class CoachesListingCatalogModel
    {
        public string OwnerId { get; set; }

        public ICollection<CoachInListCatalogModel> Coaches { get; set; } = new List<CoachInListCatalogModel>();
    }
}
