namespace UpSkill.Infrastructure.Models.Language
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LanguageCreateInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
