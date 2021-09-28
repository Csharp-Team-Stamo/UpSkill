namespace UpSkill.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class Invoice<T1, T2> : BaseModel<string>
		where T1 : class, new()
		where T2 : class, new()
	{
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
