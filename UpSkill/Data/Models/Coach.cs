namespace UpSkill.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Data.Common.Models;
    using static DataConstants;

    public class Coach : BaseDeletableModel<string>
    {
        public Coach()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string AvatarImgUrl { get; set; }
        public string Email { get; set; }

        [Required]
        //ToDo Change to CompanyName

        public string CompanyName { get; set; }

        [Required]
        public string CompanyLogoUrl { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }



        [Required]
        public string VideoUrl { get; set; }

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

        [Required]
        public string CalendlyPopupUrl { get; set; }

        public ICollection<CoachLanguage> Languages { get; set; } = new HashSet<CoachLanguage>();

        public ICollection<CoachOwner> Owners { get; set; } = new HashSet<CoachOwner>();

        public ICollection<CoachEmployee> Students { get; set; } = new HashSet<CoachEmployee>();

        public ICollection<LiveSession> LiveSessions { get; set; } = new HashSet<LiveSession>();

        public ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
