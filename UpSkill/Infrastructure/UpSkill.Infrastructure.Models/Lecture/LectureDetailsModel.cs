﻿namespace UpSkill.Infrastructure.Models.Lecture
{
    using System.Collections.Generic;

    public class LectureDetailsModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public ICollection<string> Resources { get; set; } = new List<string>();
    }
}
