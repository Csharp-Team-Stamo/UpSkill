namespace UpSkil.Tests.Integration.Data
{
using System.Collections.Generic;
using UpSkill.Data.Models;
    public static class LanguagesTestData
    {
        public static List<Language> GetDbData()
        => new()
        {
            new()
            {
                Id = 1,
                Name = "English"
            },
            new()
            {
                Id = 2,
                Name = "Spanish"
            },
            new()
            {
                Id = 3,
                Name = "German"
            }
        };

        public static List<string> GetResultData()
        => new()
        {
            "English",
            "Spanish",
            "German"
        };

    }
}
