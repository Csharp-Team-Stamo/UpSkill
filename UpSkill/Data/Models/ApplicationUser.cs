namespace UpSkill.Data.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.UserConstants;

	using Microsoft.AspNetCore.Identity;

	using UpSkill.Data.Common.Models;

	public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
	{
        [Required]
        [MaxLength(UserFullNameMaxLen)]
		public string FullName { get; set; }

        public string ImageToBase64 { get; set; }

        public int CompanyId { get; set; }
		public Company Company { get; set; }
        public string Summary { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }

		// Deletable entity
		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }
        

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = 
            new HashSet<IdentityUserClaim<string>>();
    }
}
