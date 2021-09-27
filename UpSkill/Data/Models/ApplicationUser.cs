namespace UpSkill.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.ComponentModel.DataAnnotations;
    using UpSkill.Infrastructure.Data.Common.Models;

    public abstract class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        // TODO: Add company logo property

        [Required]
        public string FullName { get; set; }

        [Required]
        public string CompanyId { get; set; }
        public Company Company { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }



        // Can be used at a later time:
        //public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        //public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        //public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
