namespace UpSkill.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	using UpSkill.Data.Common.Models;

	public class Course : BaseModel<int>
	{
		public Course()
		{
			this.StudentCourses = new HashSet<StudentCourse>();
		}

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public int CategoryId { get; set; }
		public CourseCategory Category { get; set; }

		[Required]
		public decimal Price { get; set; }

		// Decide if the video is going to be accessed through a link or not
		[Required]
		public string VideoUrl { get; set; }

		[Required]
		public string CoachId { get; set; }
		public Coach Coach { get; set; }

		public ICollection<StudentCourse> StudentCourses { get; set; }
	}
}
