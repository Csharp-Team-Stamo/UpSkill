namespace UpSkill.Data.Models
{
	using System.Collections.Generic;

	using UpSkill.Data.Common.Models;

	public class Owner : BaseModel<int>
	{
		public Owner()
		{
			this.Employees = new HashSet<Employee>();
		}

		public string UserId { get; init; }
		public ApplicationUser User { get; init; }

		public int CompanyId { get; init; }
		public Company Company { get; init; }

		public ICollection<Employee> Employees { get; set; }
	}
}
