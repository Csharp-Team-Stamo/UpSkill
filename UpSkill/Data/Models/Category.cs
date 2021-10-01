﻿namespace UpSkill.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using UpSkill.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
