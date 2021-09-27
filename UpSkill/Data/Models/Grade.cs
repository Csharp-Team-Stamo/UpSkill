namespace UpSkill.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Grade
	{
		[Key]
		[Required]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		public decimal Value { get; set; }

		[Required]
		public string CourseId { get; set; }
		public Course Course { get; set; }

		[Required]
		public string StudentId { get; set; }
		public Employee Student { get; set; }
	}
}
