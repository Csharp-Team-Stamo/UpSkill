namespace UpSkil.Tests.Integration.ControllerTests
{
    using System.Collections.Generic;
    using MyTested.AspNetCore.Mvc;
    using Data;
    using UpSkill.Api.Controllers;
    using Xunit;
   public class LanguagesControllerTests
    {
        [Fact]
        public void ShouldReturnListOfLangueges()
        {

            MyController<LanguagesController>
                .Instance(x => x.WithData(LanguagesTestData.GetDbData()))
                .Calling(x => x.GetAll())
                .ShouldHave()
                .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
                .AndAlso()
                .ShouldReturn()
                .ResultOfType<ICollection<string>>(x => x.EqualTo(LanguagesTestData.GetResultData()));
        }
    }
}
