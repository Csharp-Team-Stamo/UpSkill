namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Coaches;

    public interface ICoachesService
    {
        CoachesListingCatalogModel GetAll(string userId);

        Task AddCoachInOwnerCoachesCollectionAsync(string coachId, string ownerId);

    }

}
