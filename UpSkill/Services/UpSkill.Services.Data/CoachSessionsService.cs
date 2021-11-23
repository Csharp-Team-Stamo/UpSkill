using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Coach.Sessions;
using UpSkill.Services.Data.Contracts;

namespace UpSkill.Services.Data
{
    public class CoachSessionsService : ICoachSessionsService
    {
        public CoachSessionsService()
        {

        }

        public Task AddSession(CoachSessionEventResponseModel coach, CoachSessionInviteeResponseModel student)
        {
            throw new NotImplementedException();
        }
    }
}
