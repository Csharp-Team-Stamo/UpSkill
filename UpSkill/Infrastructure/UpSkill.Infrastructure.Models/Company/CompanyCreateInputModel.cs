namespace UpSkill.Infrastructure.Models.Company
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Common.Attributes;

    public class CompanyCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        // [StringLength(UIC_Length)]
        public string UIC { get; set; }

        public string Address { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }
    }
}
