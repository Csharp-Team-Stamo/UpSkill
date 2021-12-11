using System.Globalization;
using UpSkill.Data.Models;

namespace UpSkil.Tests.Integration.Data
{
    public static class OwnerDashboardTestData
    {
        //Test data constants
        public const string validUserId = "validUserId";
        public const string validUserIdWhoIsOwner = "validUserOwner";
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
                Id = "validUserOwner",
                FullName = "Valid Owner",
                CompanyId = 1,
                Email = "validEmail@upskill.com",
            };
        public static Owner GetValidOwner()
            => new()
            {
                Id = "validOwner",
                UserId = "validUserOwner"
            };

        public static Employee GetEmployee()
            => new()
            {
                Id = "validEmployee",
                OwnerId = "validOwner",
            };

        public static Coach GetCoach()
            => new()
            {
                Id = "validCoach",
                FullName = "Valid Coach",
            };


        public static Course GetCourse()
            => new()
            {
              Id = 1,
              Name = "validCourse",
              Price = 50
            };

        public static EmployeeCourse GetEmployeeCourses()
            => new()
            {
                CourseId = 1,
                StudentId = "validEmployee",
                CreatedOn = System.DateTime.ParseExact("03/07/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
            };

        public static LiveSession GetCoachSessions()
            => new()
            {
                StudentId = "validEmployee",
                CoachId = "validCoach",
                CreatedOn = System.DateTime.ParseExact("03/07/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Price = 50,
            };
    }
}

//invoiceInfo.MonthlyCoursesToPay = await this.coursesRepo
//                .All()
//                .Where(ec =>
//                            ec.Student.Owner.Id == ownerId &&
//                            ec.CreatedOn.Month == monthNum)
//                .Select(ec => new CourseInvoiceModel
//                {
//                    CourseId = ec.Course.Id,
//                    EmployeeId = ec.Student.Id,
//                    CourseName = ec.Course.Name,
//                    IssueDate = ec.CreatedOn,
//                    Price = ec.Course.Price
//                })
//                .ToListAsync();

//invoiceInfo.MonthlySessionsToPay = await this.sessionRepo
//    .All()
//    .Where(s =>
//                s.Student.Owner.Id == ownerId &&
//                s.End.Month == monthNum)
//    .Select(s => new LiveSessionInvoiceModel
//    {
//        SessionId = s.Id,
//        EmployeeId = s.Student.Id,
//        CoachName = s.Coach.FullName,
//        Price = s.Price,
//        IssueDate = s.End
//    })
//    .ToListAsync();
