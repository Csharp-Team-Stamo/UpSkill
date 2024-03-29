﻿namespace UpSkill.Infrastructure.Models.Employee
{
    using System.ComponentModel.DataAnnotations;
    using Common.Attributes;
    using static Common.GlobalConstants.AddUserForm;

    public class AddEmployeeFormModel
    {
        public string UserId { get; set; }

        public string CompanyId { get; set; }

        [Required]
        [MinLength(FullNameMinLen, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(FullNameMaxLen, ErrorMessage = "Name can be maximum 20 characters")]
        public string FullName { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }

        public bool AddAnotherOneBtn { get; set; } = false;
    }
}
