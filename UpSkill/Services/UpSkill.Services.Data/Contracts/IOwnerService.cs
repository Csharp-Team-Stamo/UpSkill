namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using Infrastructure.Models.Owner;

    public interface IOwnerService
    {
        Task<string> GetId(string userId);
        Task<OwnerInvoiceDetailsModel> GetInvoiceInfo(string ownerId, int monthNum);
        Task<string> GetOwnerId(string userId);
    }
}
