namespace UpSkill.Data.Models
{
    using UpSkill.Data.Common.Models;

    public class InvoiceStatus : BaseDeletableModel<int>
    {
        public string Name { get; init; }
    }
}
