
namespace UpSkil.Tests.Integration.ControllerTests
{
    using MyTested.AspNetCore.Mvc;
    using UpSkil.Tests.Integration.Data;
    using UpSkill.Api.Controllers;
    using UpSkill.Infrastructure.Models.Owner;
    using Xunit;
    public class OwnerDashboardTests : MyController<OwnerDashboardController>
    {
        [Fact]
        public void IfMonthIsInvalidShouldReturnBadRequest()
        {
            Instance()
            .Calling(x => x.GetInvoiceInfo(OwnerDashboardTestData.validUserId, OwnerDashboardTestData.invalidMonth))
            .ShouldHave()
            .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .BadRequest(x => x.WithErrorMessage(OwnerDashboardTestData.InvalidIdOrMonthErrorMessage));
        }

        [Fact]
        public void IfUserIsNullReturnBadRequest()
        {
            Instance(x => x.WithData(OwnerDashboardTestData.GetValidUser()))
            .Calling(x => x.GetInvoiceInfo(OwnerDashboardTestData.invalidId, OwnerDashboardTestData.validMonth))
            .ShouldHave()
            .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .BadRequest(x => x.WithErrorMessage(OwnerDashboardTestData.UserDoesNotExist)); ;
        }

        [Fact]
        public void IfUserIsNotOwnerReturnBadRequrest()
        {
            Instance(x => x.WithData(OwnerDashboardTestData.GetValidUser(), OwnerDashboardTestData.GetValidOwner()))
            .Calling(x => x.GetInvoiceInfo(OwnerDashboardTestData.validUserId, OwnerDashboardTestData.validMonth))
            .ShouldHave()
            .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .BadRequest(x => x.WithErrorMessage(OwnerDashboardTestData.UserIsNotOwner));
        }

        [Fact]
        public void OnCorrectInputShouldReturnOwnerInvoiceDetailsModel()
        {
            Instance(x =>
            x.WithData(
                OwnerDashboardTestData.GetValidUserWhoIsOwner(),
                OwnerDashboardTestData.GetCoach(),
                OwnerDashboardTestData.GetCoachSessions(),
                OwnerDashboardTestData.GetEmployee(),
                OwnerDashboardTestData.GetCourse(),
                OwnerDashboardTestData.GetEmployeeCourses(),
                OwnerDashboardTestData.GetValidOwner()))
            .Calling(x => x.GetInvoiceInfo(OwnerDashboardTestData.validUserIdWhoIsOwner, OwnerDashboardTestData.validMonth))
            .ShouldHave()
            .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .Object(x => x.WithModelOfType<OwnerInvoiceDetailsModel>());
        }

        [Theory]
        [InlineData(null, 1)]
        [InlineData("", 1)]
        [InlineData(OwnerDashboardTestData.validUserId, OwnerDashboardTestData.invalidMonth)]
        [InlineData(OwnerDashboardTestData.validUserId, -1)]
        [InlineData(OwnerDashboardTestData.validUserId, 0)]
        public void IfUserIdIsNullShouldReturnBadRequest(string userId, int month)
        {
            Instance()
            .Calling(x => x.GetInvoiceInfo(userId, month))
            .ShouldHave()
            .ActionAttributes(x => x.RestrictingForHttpMethod(HttpMethod.Get))
            .AndAlso()
            .ShouldReturn()
            .BadRequest(x => x.WithErrorMessage(OwnerDashboardTestData.InvalidIdOrMonthErrorMessage));
        }
    }
}
