namespace UpSkill.Data.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Course
	{
		public Course()
		{
			this.StudentCourses = new HashSet<StudentCourse>();
		}

		[Key]
		[Required]
		public string Id { get; init; } = Guid.NewGuid().ToString();

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		public Category Category { get; set; }

		[Required]
		public decimal Price { get; set; }

		// Decide if the video is going to be accessed through a link or not
		[Required]
		public string VideoURL { get; set; }

		[Required]
		public string CoachId { get; set; }
		public Coach Coach { get; set; }

		public ICollection<StudentCourse> StudentCourses { get; set; }
	}
}
