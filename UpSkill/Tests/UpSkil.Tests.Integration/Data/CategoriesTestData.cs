namespace UpSkil.Tests.Integration.Data
{
    using System.Collections.Generic;
    using UpSkill.Data.Models;

    public static class CategoriesTestData
    {
        public static List<Category> GetCategories()
        {
            return new()
            {
                new Category
                {
                    Id = 1,
                    Name = "Category1"
                },
                new Category
                {
                    Id = 2,
                    Name = "Category2"
                },
            };
        }
    }
}
