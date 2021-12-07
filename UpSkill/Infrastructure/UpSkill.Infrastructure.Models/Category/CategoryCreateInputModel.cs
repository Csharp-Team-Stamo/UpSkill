using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Category
{
    public class CategoryCreateInputModel
    {
        private const string RequiredErrorMessage = "Category name is required";
        private const int NameMinLen = 2;
        private const int NameMaxLen = 30;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLen, 
            MinimumLength = NameMinLen, 
            ErrorMessage = "Name should be {1} to {0} characters long")]
        public string Name { get; set; }
    }
}
