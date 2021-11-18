namespace UpSkill.Infrastructure.Models.ApplicationUser
{
    using System.ComponentModel.DataAnnotations;
    using Common.Attributes;
    using static Common.GlobalConstants.AddUserForm;

    public class EditApplicationUserModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(FullNameMinLen, ErrorMessage = "Name must be at least 2 characters")]
        [MaxLength(FullNameMaxLen, ErrorMessage = "Name can be maximum 20 characters")]
        public string FullName { get; set; }

        [Required]
        [IsEmail]
        public string Email { get; set; }

        [MaxLength(SummaryMaxLen, ErrorMessage = "Name can be maximum 50 characters")]
        public string Summary { get; set; }

        public string CompanyName { get; set; }

        //ToDo add size check
        public string ImageToBase64 { get; set; }
    }
}
