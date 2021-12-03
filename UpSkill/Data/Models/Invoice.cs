namespace UpSkill.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;
    using static DataConstants.InvoiceConstants;

    public class Invoice : BaseDeletableModel<string>
    {
        public int StatusId { get; init; }
        public InvoiceStatus Status { get; init; }

        [Required]
        public string BuyerId { get; set; }
        public Owner Buyer { get; set; }

        public DateTime DueDate { get; init; }

        public decimal Price { get; set; }

        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
