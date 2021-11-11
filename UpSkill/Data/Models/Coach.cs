﻿namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static UpSkill.Data.DataConstants;

    public class Coach : BaseDeletableModel<string>
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        [Required]
        [RegularExpression(CoachConstants.ValidImage, ErrorMessage = "Not valid url! Valid extensions(png, jpg, jpeg, gif)")]
        public string AvatarImgUrl { get; set; }

        [Required]
        [MaxLength(CoachConstants.SessionDescriptionMaxlen)]
        public string SessionDescription { get; set; }

        [Required]
        [MaxLength(CoachConstants.SkillsLearnMaxlen)]
        public string SkillsLearn { get; set; }

        [Required]
        public string DiscussionDurationInMinutes { get; set; }

        [Required]
        public string ResourcesCount { get; set; }

        [Required]
        public decimal PricePerSession { get; set; }

        public ICollection<CoachLanguage> Languages { get; set; } = new HashSet<CoachLanguage>();

        public ICollection<CoachOwner> Owners { get; set; } = new HashSet<CoachOwner>();

        public ICollection<CoachEmployee> Students { get; set; } = new HashSet<CoachEmployee>();

        public ICollection<CoachVote> Votes { get; set; } = new HashSet<CoachVote>();

        public ICollection<LiveSession> LiveSessions { get; set; } = new HashSet<LiveSession>();

        public ICollection<SessionSlot> SessionSlots { get; set; } = new HashSet<SessionSlot>();

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
