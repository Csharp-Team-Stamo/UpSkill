﻿namespace UpSkill.Data.Models
{
    using Common.Models;

    public class CourseOwner
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string OwnerId { get; set; }
        public Owner Owner { get; set; }
    }
}
