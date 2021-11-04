namespace UpSkill.Infrastructure.Common
{
    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";
        public const string BusinessOwnerRoleName = "Owner";
        public const string EmployeeRoleName = "Employee";

        public const string EmailRegEx = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public const string demoMessage = "Dear {0}, thank you for contacting us. Below you can find a link where you can test the functionalities of the website";

        public const string demoMessageHtml = "<h3>Dear {0},</h3> <p>Thank you for contacting us.</p> Below you can find a link where you can test the functionalities of the website:<p><a>Link</a></p><p>Kind regards,</p><p>Upskill Team</p>";

        public const string demoSubject = "UpSkill Demo Request";

        public class WelcomePageConst
        {
            public const int NameMinLen = 2;
            public const int NameMaxLen = 20;

            public const int CompanyNameMinLen = 2;
            public const int CompanyNameMaxLen = 20;

        }

        public class AddEmployeeModal
        {
            public const int FullNameMinLen = 2;
            public const int FullNameMaxLen = 20;
        }

        public class Errors
        {
            public const string InvalidEmail = "Not valid email";
        }
    }
}
