using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Coach;

namespace UpSkill.Services.Data.Contracts
{
    public interface ICoachService
    {
        Task<Coach> Create(CoachCreateInputModel coachInput);
        Task<Coach> GetCoach(CoachCreateInputModel coachInput);
        Task<IEnumerable<AdminCoachListingServiceModel>> GetAll();
    }
}
