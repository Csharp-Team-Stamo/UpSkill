
namespace UpSkill.Services.Data
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UpSkill.Data.Common.Repositories;
using UpSkill.Data.Models;
using UpSkill.Infrastructure.Models.Coach.Sessions;
using UpSkill.Services.Data.Contracts;

    public class CoachSessionsService : ICoachSessionsService
    {
        private readonly IDeletableEntityRepository<Coach> coachRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public CoachSessionsService(
            IDeletableEntityRepository<Coach> coachRepository,
            IDeletableEntityRepository<Employee> employeeRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.coachRepository = coachRepository;
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
        }

        public Task AddSession(CoachSessionEventResponseModel session, CoachSessionInviteeResponseModel invitee, string coachCalendlyUri)
        {
            var userId = userRepository.All().FirstOrDefault(x => x.Email == invitee.Email).Id;
            var coach = coachRepository.All().FirstOrDefault(x => x.CalendlyPopupUrl == coachCalendlyUri);
            var student = employeeRepository.All().FirstOrDefault(x => x.UserId == userId);

            var coachSession = new LiveSession()
            {
                CoachId = coach.Id,
                StudentId = student.Id,
                Start = session.StartTime,
                End = session.EndTime,
                Price = coach.PricePerSession,
                GivenFeedback = false
            };

            throw new NotImplementedException();
        }
    }
}
