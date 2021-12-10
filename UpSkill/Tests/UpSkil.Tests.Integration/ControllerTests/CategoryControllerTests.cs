namespace UpSkil.Tests.Integration.ControllerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using Data;
    using MyTested.AspNetCore.Mvc;
    using UpSkill.Api.Controllers;
    using Xunit;
    using HttpMethod = System.Net.Http.HttpMethod;

    public class CategoryControllerTests
    {
        [Fact]
        public void ShouldReturnCorrectModel()
        {
            MyController<CategoryController>
                .Instance(x => x.WithData(CategoriesTestData.GetCategories()))
                .Calling(x => x.GetAllNames())
                .ShouldHave()
                .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .Ok(x => x.WithModelOfType<ICollection<string>>()
                    .Passing(x => x.Any(x => x == "Category1")));
        }
    }
}
