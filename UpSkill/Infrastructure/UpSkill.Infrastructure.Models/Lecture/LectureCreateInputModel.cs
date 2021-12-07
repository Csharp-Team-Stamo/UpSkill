﻿using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Lecture
{
    public class LectureCreateInputModel
    {
        private const string RequiredErrorMessage = " is required.";
        private const int TitleMinLen = 2;
        private const int TitleMaxLen = 25;
        private const int DescriptionMinLen = 10;
        private const int DescriptionMaxLen = 250;

        [Required(ErrorMessage = nameof(CourseId) + RequiredErrorMessage)]
        [Display(Name = "Course Id")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = nameof(Title) + RequiredErrorMessage)]
        [MinLength(TitleMinLen)]
        [MaxLength(TitleMaxLen)]
        public string Title { get; set; }

        [Required(ErrorMessage = nameof(Description) + RequiredErrorMessage)]
        [MinLength(DescriptionMinLen)]
        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required(ErrorMessage = nameof(VideoUrl) + RequiredErrorMessage)]
        [Url]
        [Display(Name = "Video URL")]
        public string VideoUrl { get; set; }
    }
}
