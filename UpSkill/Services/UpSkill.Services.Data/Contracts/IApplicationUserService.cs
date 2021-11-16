namespace UpSkill.Services.Data.Contracts
{
    using Infrastructure.Models.ApplicationUser;

    public interface IApplicationUserService
    {
        EditApplicationUserModel GetById(string userId);
    }
}
