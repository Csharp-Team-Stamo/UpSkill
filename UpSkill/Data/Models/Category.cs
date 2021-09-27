namespace UpSkill.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Category
	{
		[Key]
		[Required]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		[Required]
		public string Name { get; set; }
	}
}
