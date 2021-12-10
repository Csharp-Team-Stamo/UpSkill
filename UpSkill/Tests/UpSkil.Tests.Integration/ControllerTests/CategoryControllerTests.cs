namespace UpSkil.Tests.Integration.ControllerTests
{
    using System.Collections.Generic;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using UpSkill.Api.Controllers;
    using UpSkill.Data.Models;
    using Xunit;

    public class CategoryControllerTests
    {
        [Fact]
        public void ShouldReturnCorrectModel()
        {
            MyController<CategoryController>
                .Instance(x => x.WithData(CategoriesTestData.GetCategories()))
                .Calling(x => x.GetAllNames())
                .ShouldReturn()
                .ResultOfType<ICollection<string>>();
                
        }
    }
}
