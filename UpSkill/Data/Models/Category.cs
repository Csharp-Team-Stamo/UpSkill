namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;
    using static DataConstants.CategoryConstants;

    public class Category : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }
    }
}
