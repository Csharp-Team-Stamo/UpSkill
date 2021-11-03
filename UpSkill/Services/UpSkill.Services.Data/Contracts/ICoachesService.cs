namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coaches;

    public interface ICoachesService
    {
        CoachesListingCatalogModel GetAll(string userId);

        CoachesListingCatalogModel GetAllByOwnerId(string userId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

        Task RemoveCoachFromOwnerCoachCollectionAsync(string coachId, string userId);
    }

}
