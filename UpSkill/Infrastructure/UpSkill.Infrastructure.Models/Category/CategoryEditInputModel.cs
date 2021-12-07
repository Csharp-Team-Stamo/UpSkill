using System.ComponentModel.DataAnnotations;

namespace UpSkill.Infrastructure.Models.Category
{
    public class CategoryEditInputModel
    {
        private const string RequiredErrorMessage = "Category name is required";
        private const int NameMinLen = 2;
        private const int NameMaxLen = 30;

        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(NameMaxLen,
            MinimumLength = NameMinLen,
            ErrorMessage = "Name should be {1} to {0} characters long")]
        public string Name { get; set; }
    }
}
