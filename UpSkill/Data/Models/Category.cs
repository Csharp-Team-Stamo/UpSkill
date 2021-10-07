namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
