namespace UpSkill.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class LiveSessionCategory : BaseModel<int>
	{
		public LiveSessionCategory()
		{
			this.Sessions = new HashSet<LiveSession>();
		}

		[Required]
		public string Name { get; set; }

		public ICollection<LiveSession> Sessions { get; init; }
	}
}
