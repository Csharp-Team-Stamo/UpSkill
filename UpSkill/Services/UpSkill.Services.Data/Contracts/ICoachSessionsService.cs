using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Coach.Sessions;

namespace UpSkill.Services.Data.Contracts
{
    public interface ICoachSessionsService
    {
        Task AddSession(CoachSessionEventResponseModel session, CoachSessionInviteeResponseModel student, string coachCalendlyUri);


    }
}

