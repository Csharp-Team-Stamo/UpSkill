namespace UpSkill.Infrastructure.Models.Company
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CompanyCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        // [StringLength(UIC_Length)]
        public string UIC { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
