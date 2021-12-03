namespace UpSkill.Data
{
    public class DataConstants
    {
        public class CategoryConstants
        {
            public const int NameMaxLen = 30;
        }

        public class CoachConstants
        {
            public const int SessionDescriptionMaxlen = 150;

            public const int SkillsLearnMaxlen = 150;
            public const int CompanyNameMaxLen = 30;
            public const int ResourceCountMaxLen = 3;
        }

        public class CoachFeedBackConstants
        {
            public const int FeedBackMaxLen = 150;
        }

        public class Company
        {
            public const int UIC_Length = 9;
            public const int NameMaxLen = 30;
        }

        public class CourseConstants
        {
            public const int NameMaxLen = 40;
            public const int SessionDescriptionMaxlen = 150;
            public const int SkillsLearnMaxlen = 150;
            public const int CompanyNameMaxLen = 30;
            public const int CourseDurationInHoursMaxLen = 3;
            public const int LecturesNumMaxLen = 3;
            public const int AuthorNameMaxLen = 30;
        }

        public class InvoiceConstants
        {
            public const int DescriptionMaxLen = 150;
        }

        public class InvoiceStatusConstants
        {
            public const int NameMaxLen = 15;
        }

        public class LanguageConstants
        {
            public const int NameMaxLen = 20;
        }

        public class LectureConstants
        {
            public const int TitleMaxLen = 25;
            public const int DescriptionMaxLen = 250;
        }

        public class LectureResourceConstants
        {
            public const int TitleMaxLen = 50;
        }

        public class PriceContants
        {
            public const int Precision = 18;

            public const int Scale = 4;
        }

        public class UserConstants
        {
            public const int UserFullNameMaxLen = 50;
        }
    }
}
