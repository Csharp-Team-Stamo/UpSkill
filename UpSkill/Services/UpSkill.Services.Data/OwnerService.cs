namespace UpSkill.Services.Data
{
    using System.Linq;
    using Contracts;
    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;

    public class OwnerService : IOwnerService
    {
        private readonly IDeletableEntityRepository<Owner> ownerRepository;

        public OwnerService(IDeletableEntityRepository<Owner> ownerRepository)
        {
            this.ownerRepository = ownerRepository;
        }

        public string GetId(string userId)
        {
            return ownerRepository.All().FirstOrDefault(x => x.UserId == userId)?.Id;
        }
    }
}
