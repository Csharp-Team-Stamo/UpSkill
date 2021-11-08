namespace UpSkill.Infrastructure.Models.Coaches
{
using System.Collections.Generic;
   public class SortingTermsCoachesModel
    {
        public ICollection<string> Categories { get; set; } = new List<string>();

        public ICollection<string> Languages { get; set; } = new List<string>();

    }
}
