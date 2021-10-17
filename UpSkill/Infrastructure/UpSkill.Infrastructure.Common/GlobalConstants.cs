﻿namespace UpSkill.Infrastructure.Common
{
    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";
        public const string BusinessOwnerRoleName = "Owner";
        public const string EmployeeRoleName = "Employee";

        public class WelcomePageConst
        {
            public const int NameMinLen = 2;
            public const int NameMaxLen = 20;

            public const int CompanyNameMinLen = 2;
            public const int CompanyNameMaxLen = 20;

            public const string EmailRegEx = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        }
    }
}
