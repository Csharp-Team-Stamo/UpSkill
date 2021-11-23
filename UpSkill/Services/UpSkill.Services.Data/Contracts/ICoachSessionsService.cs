using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSkill.Infrastructure.Models.Coach.Sessions;

namespace UpSkill.Services.Data.Contracts
{
    public interface ICoachSessionsService
    {
        Task AddSession(CoachSessionEventResponseModel coach, CoachSessionInviteeResponseModel student);


    }
}

