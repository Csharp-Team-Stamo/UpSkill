namespace UpSkill.Data.Models
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class LiveSession : BaseModel<string>
	{
		[Required]
		public string Topic { get; set; }

		public decimal Price { get; set; }

		public DateTime StartOfSession { get; set; }

		public DateTime EndOfSession { get; set; }

		public TimeSpan Duration { get; set; }

		[Required]
		public string CoachId { get; set; }
		public Coach Coach { get; set; }

		[Required]
		public string StudentId { get; set; }
		public Employee Student { get; set; }
	}
}
