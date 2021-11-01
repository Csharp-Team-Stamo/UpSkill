namespace UpSkill.Infrastructure.Models.Coaches
{
    using System.Collections;
    using System.Collections.Generic;

    public class CoachesListingCatalogModel
    {
        public ICollection<CoachInListingCatalogModel> Coaches { get; set; }
    }
}
