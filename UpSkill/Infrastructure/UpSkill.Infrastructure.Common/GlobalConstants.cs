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

        public const string resetPasswordMessage = "<h1>Dear {0},</h1><p> You told us you forgot your password. If you really did, click here to choose a new one:</p><p><a href='{1}' >Click here to reset</a></p><p>If you didn't request this email message, please ignore it!</p><p>Kind regards,</p><p>Upskill Team</p>";

        public const string verifyAndResetPassword = "<h3>Dear {0},</h3> <p>To verify your email and to set up your password, please follow the following <a href='{1}'style='font-weight:bold'>link</a>.</p><p>If you didn't request this email message, please ignore it!</p><p>Kind regards,</p><p>Upskill Team</p>";

        public const string demoSubject = "UpSkill Demo Request";

        public const string coachSessionSubject = "New UpSkill Coach Session: {0} - {1} - {2}";
        public const string coachSessionNotificationAsHtml = "<div style='text-align: left;'> <h3>Dear {0},</h3> <p>You have a new scheduled event!</p> <p><span style='font-weight:bold'>Event type:</span> {1} </p> <p><span style='font-weight:bold'>Invitee:</span> {2} </p> <p><span style='font-weight:bold'>Invitee email:</span> {3} </p> <p><span style='font-weight:bold'>Event Time/Date:</span> {4} </p> <p><span style='font-weight:bold'>Location:</span>This is a Google Meet web call. <a href='{5}' style='font-weight:bold'>Join now</a></p> <p>Kind regards,</p> <p>Upskill Team</p></div>";

        public class WelcomePageConst
        {
            public const int NameMinLen = 2;
            public const int NameMaxLen = 20;

            public const int CompanyNameMinLen = 2;
            public const int CompanyNameMaxLen = 20;

        }

        public class AddUserForm
        {
            public const int FullNameMinLen = 2;
            public const int FullNameMaxLen = 20;

            public const int SummaryMaxLen = 50;
        }

        public class Errors
        {
            public const string InvalidEmail = "Not valid email";
            public const string EmailIsTaken = "This email is already taken!";


        }
    }
}
