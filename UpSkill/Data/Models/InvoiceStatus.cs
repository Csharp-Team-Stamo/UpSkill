namespace UpSkill.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static DataConstants.InvoiceStatusConstants;

    public class InvoiceStatus : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; init; }
    }
}
