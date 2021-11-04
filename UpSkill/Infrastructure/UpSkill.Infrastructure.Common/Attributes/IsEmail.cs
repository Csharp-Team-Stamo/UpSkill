namespace UpSkill.Infrastructure.Common.Attributes
{
    using System.ComponentModel.DataAnnotations;

    using System.Text.RegularExpressions;

    using static UpSkill.Infrastructure.Common.GlobalConstants;
    using static UpSkill.Infrastructure.Common.GlobalConstants.Errors;

    public class IsEmail : ValidationAttribute
    {
        public IsEmail()
            : base()
        {
            this.ErrorMessage = InvalidEmail;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var email = (string)value;
            var regex = new Regex(EmailRegEx);

            if (regex.IsMatch(email))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}
