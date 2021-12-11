using UpSkill.Data.Models;

namespace UpSkil.Tests.Integration.Data
{
    public static class OwnerDashboardTestData
    {
        //Test data constants
        public const string validUserId = "validUserId";
        public const string validUserIdWhoIsOwner = "validUserId";
        public const string nullId = null;
        public const string invalidId = "userDoesNotExist";
        public const int validMonth = 7;
        public const int invalidMonth = 13;

        //Error messages
        public const string InvalidIdOrMonthErrorMessage = "Valid Owner Id and month number is required.";
        public const string UserDoesNotExist = "Owner does not exist.";
        public const string UserIsNotOwner = "The User is not an Owner.";

        public static ApplicationUser GetValidUser()
            => new()
            {
                Id = validUserId,
                FullName = "Valid User",
                CompanyId = 1,
                Email = "validEmail@upskill.com",
            };

        public static ApplicationUser GetValidUserWhoIsOwner()
            => new()
            {
                Id = "Owner",
                FullName = "Valid Owner",
                CompanyId = 1,
                Email = "validEmail@upskill.com",
            };
        public static Owner GetValidOwner()
            => new()
            {
                Id = "validOwner",
                UserId = "Owner"
            };
    }
}

