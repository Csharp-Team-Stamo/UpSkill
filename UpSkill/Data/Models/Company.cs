namespace UpSkill.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	using static UpSkill.Data.DataConstants.Company;

	public class Company : BaseModel<int>
	{
		public Company()
		{
			this.Employees = new HashSet<Employee>();
			this.CourseIncomeInvoices = new HashSet<Invoice<Employee, Company>>();
			this.CoachExpensesInvoices = new HashSet<Invoice<Company, Coach>>();
		}

		[Required]
		public string Name { get; set; }

		[Required]
		public string OwnerId { get; set; }
		public Owner Owner { get; set; }

		[Required]
		[StringLength(UIC_Length)]
		public string UIC { get; set; }

		[Required]
		public string Address { get; set; }

		public ICollection<Employee> Employees { get; set; }

		public ICollection<Invoice<Employee, Company>> CourseIncomeInvoices { get; set; }

		public ICollection<Invoice<Company, Coach>> CoachExpensesInvoices { get; set; }
	}
}
