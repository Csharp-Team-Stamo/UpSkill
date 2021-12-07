using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Category
{
    public class CategoryCreateInputModel
    {
        private const string RequiredErrorMessage = "Category name is required";
        private const int NameMinLen = 2;
        private const int NameMaxLen = 30;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(NameMinLen)]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }
    }
}
