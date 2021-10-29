﻿namespace UpSkill.Infrastructure.Models.AddEmployeeModal
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;
    using static Common.GlobalConstants.AddEmployeeModal;

    public class AddEmployeeFormModel
    {
        public string CompanyId { get; set; }

        [Required]
        [MinLength(FullNameMinLen, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(FullNameMaxLen, ErrorMessage = "Name can be maximum 20 characters")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(EmailRegEx, ErrorMessage = "Not valid email")]
        public string Email { get; set; }

        public bool AddAnotherOneBtn { get; set; } = false;
    }
}
