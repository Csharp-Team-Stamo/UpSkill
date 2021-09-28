namespace UpSkill.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using UpSkill.Data.Common.Models;

	public class Coach : BaseModel<string>
	{
		public Coach()
		{
			this.Courses = new HashSet<Course>();
			this.LiveSessions = new HashSet<LiveSession>();
			this.SessionSlots = new HashSet<SessionSlot>();
			this.Invoices = new HashSet<Invoice>();
		}

		public string UserId { get; init; }
		public ApplicationUser User { get; init; }

		[Required]
		public decimal PricePerSession { get; set; }

		public ICollection<Course> Courses { get; set; }

		public ICollection<LiveSession> LiveSessions { get; set; }

		public ICollection<SessionSlot> SessionSlots { get; set; }

		public ICollection<Invoice> Invoices { get; set; }
	}
}
