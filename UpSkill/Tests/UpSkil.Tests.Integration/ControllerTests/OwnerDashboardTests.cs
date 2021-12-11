
namespace UpSkil.Tests.Integration.ControllerTests
{
    using MyTested.AspNetCore.Mvc;
    using UpSkil.Tests.Integration.Data;
    using UpSkill.Api.Controllers;
    using Xunit;

    public class OwnerDashboardTests : MyController<OwnerDashboardController>
    {
        [Fact]
        public void IfUserIdIsNullShouldReturnBadRequest()
        {
            Instance()
            .Calling(x => x.GetInvoiceInfo(OwnerDashboardTestData.nullId, OwnerDashboardTestData.validMonth))
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
    }
    //public async Task<ActionResult<OwnerInvoiceDetailsModel>> GetInvoiceInfo(
    //       string userId, int monthNum)
    //{
    //    if (userId == null ||
    //        monthNum <= 0 ||
    //        monthNum > 12)
    //    {
    //        return BadRequest("Valid Owner Id and month number is required.");
    //    }
}
