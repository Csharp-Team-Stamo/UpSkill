namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.ApplicationUser;

    public interface IApplicationUserService
    {
        EditApplicationUserModel GetById(string userId);

        Task UpdateUser(EditApplicationUserModel model);
    }
}
