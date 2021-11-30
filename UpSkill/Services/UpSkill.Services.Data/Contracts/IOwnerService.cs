namespace UpSkill.Services.Data.Contracts
{
    using System.Threading.Tasks;
    using UpSkill.Infrastructure.Models.Owner;

    public interface IOwnerService
    {
        string GetId(string userId);
        Task<OwnerInvoiceDetailsModel> GetInvoiceInfo(string ownerId);
        Task<string> GetOwnerId(string userId);
    }
}
