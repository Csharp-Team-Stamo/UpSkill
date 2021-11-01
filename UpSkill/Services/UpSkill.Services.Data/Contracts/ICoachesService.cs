namespace UpSkill.Services.Data.Contracts
{
    using Infrastructure.Models.Coaches;

    public interface ICoachesService
    {
        CoachesListingCatalogModel GetAll();
    }
}
