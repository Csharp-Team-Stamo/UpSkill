namespace UpSkill.Data.Models
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

	using Microsoft.AspNetCore.Identity;

	using UpSkill.Data.Common.Models;

	public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
	{
		// TODO: Add company logo property

		[Required]
		public string FullName { get; set; }
    
		public int CompanyId { get; set; }
		public Company Company { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

		public DateTime? ModifiedOn { get; set; }

		// Deletable entity
		public bool IsDeleted { get; set; }

		public DateTime? DeletedOn { get; set; }

        // Can be used at a later time:
        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; } = new HashSet<IdentityUserClaim<string>>();

        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
