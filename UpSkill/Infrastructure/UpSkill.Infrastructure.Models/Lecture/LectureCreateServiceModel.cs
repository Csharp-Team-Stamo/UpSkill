﻿namespace UpSkill.Infrastructure.Models.Lecture
{
    public class LectureCreateServiceModel
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }
    }
}
