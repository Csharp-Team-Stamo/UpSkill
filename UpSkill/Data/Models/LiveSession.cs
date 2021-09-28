namespace UpSkill.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class LiveSession : BaseModel<string>
	{
		[Required]
		public string Topic { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

		[Required]
		public string CoachId { get; set; }
		public Coach Coach { get; set; }

		[Required]
		public string StudentId { get; set; }
		public Employee Student { get; set; }

		public LiveSessionCategory Category { get; init; }
	}
}
