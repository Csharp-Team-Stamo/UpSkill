namespace UpSkill.Data.Models
{
	using System;
	using System.Collections.Generic;

	using UpSkill.Data.Common.Models;

	public class Coach : BaseModel<string>
	{
		public Coach()
		{
			this.Courses = new HashSet<Course>();
			this.LiveSessions = new HashSet<LiveSession>();
			this.DatesAvailable = new HashSet<DateTime>();
			this.Invoices = new HashSet<Invoice>();
		}

		public decimal PricePerSession { get; set; }

		public ICollection<Course> Courses { get; set; }

		public ICollection<LiveSession> LiveSessions { get; set; }

		public ICollection<DateTime> DatesAvailable { get; set; }

		public ICollection<Invoice> Invoices { get; set; }
	}
}
