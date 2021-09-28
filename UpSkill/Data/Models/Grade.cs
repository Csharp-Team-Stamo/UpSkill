namespace UpSkill.Data.Models
{
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class Grade : BaseModel<int>
	{
		[Required]
		public decimal Value { get; set; }

		[Required]
		public string CourseId { get; set; }
		public Course Course { get; set; }

		[Required]
		public string StudentId { get; set; }
		public Employee Student { get; set; }
	}
}
