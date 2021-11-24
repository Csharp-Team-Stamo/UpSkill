namespace UpSkill.Infrastructure.Models.Sorting
{
    using System.Collections.Generic;

    public class SortingTermsCatalogsModel
    {
        public ICollection<string> Categories { get; set; } = new List<string>();

        public ICollection<string> Languages { get; set; } = new List<string>();

    }
}
