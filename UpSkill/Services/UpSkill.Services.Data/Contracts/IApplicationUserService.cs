namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.ApplicationUser;

    public interface IApplicationUserService
    {
        Task<EditApplicationUserModel> GetByIdAsync(string userId);

        Task UpdateUserAsync(EditApplicationUserModel model);
    }
}
