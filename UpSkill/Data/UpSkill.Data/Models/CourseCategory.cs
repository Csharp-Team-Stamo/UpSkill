namespace UpSkill.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class CourseCategory : BaseModel<int>
	{
		public CourseCategory()
		{
			this.Courses = new HashSet<Course>();
		}

		[Required]
		public string Name { get; set; }

		public ICollection<Course> Courses { get; init; }
	}
}
