namespace UpSkill.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Invoice<T1, T2>
		where T1 : class, new()
		where T2 : class, new()
	{
		[Key]
		[Required]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		[Required]
		public string BuyerId { get; set; }
		public T1 Buyer { get; set; }

		[Required]
		public string SellerId { get; set; }
		public T2 Seller { get; set; }

		public decimal Sum { get; set; }

		[Required]
		public string Description { get; set; }

		public DateTime TransactionTime { get; set; }
	}
}
