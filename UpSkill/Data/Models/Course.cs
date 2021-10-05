﻿namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Data.Models;

    using UpSkill.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.StudentCourses = new HashSet<EmployeeCourse>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public decimal Price { get; set; }

        // Decide if the video is going to be accessed through a link or not
        [Required]
        public string VideoUrl { get; set; }

        public string CoachId { get; set; }
        public Coach Coach { get; set; }

        public ICollection<CourseOwner> Owners { get; set; }

        public ICollection<CourseVote> Votes { get; set; }

        public ICollection<EmployeeCourse> StudentCourses { get; set; }
    }
}
