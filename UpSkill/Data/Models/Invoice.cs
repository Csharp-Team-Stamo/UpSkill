namespace UpSkill.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class Invoice : BaseDeletableModel<string>
    {
        public int StatusId { get; init; }
        public InvoiceStatus Status { get; init; }

        [Required]
        public DateTime DueDate { get; init; }

        public string BuyerId { get; set; }
        public Owner Buyer { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime TransactionTime { get; set; }
    }
}
