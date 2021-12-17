
namespace UpSkill.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using UpSkill.Data.Common.Repositories;
    using UpSkill.Data.Models;
    using Infrastructure.Models.Coach.Sessions;
    using Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CoachSessionsService : ICoachSessionsService
    {
        private readonly IDeletableEntityRepository<Coach> coachRepository;
        private readonly IDeletableEntityRepository<Employee> employeeRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<LiveSession> liveSessionRepository;
        private readonly IEmailSender emailSender;

        public CoachSessionsService(
            IDeletableEntityRepository<Coach> coachRepository,
            IDeletableEntityRepository<Employee> employeeRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<LiveSession> liveSessionRepository,
            IEmailSender emailSender)
        {
            this.coachRepository = coachRepository;
            this.employeeRepository = employeeRepository;
            this.userRepository = userRepository;
            this.liveSessionRepository = liveSessionRepository;
            this.emailSender = emailSender;
        }

        public async Task AddSession(CoachSessionEventResponseModel session, CoachSessionInviteeResponseModel invitee, string coachCalendlyUri)
        {
            //var userId = this.userRepository.All().FirstOrDefault(x => x.Email == invitee.Email).Id;

            var userId = await this.userRepository.All().Where(x => x.Email == invitee.Email).Select(x => x.Id).FirstOrDefaultAsync();

            var coach = await this.coachRepository.All().FirstOrDefaultAsync(x => x.CalendlyPopupUrl == coachCalendlyUri);
            var student = await this.employeeRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);

            var coachSession = new LiveSession()
            {
                CoachId = coach.Id,
                StudentId = student.Id,
                Start = session.StartTime,
                End = session.EndTime,
                Price = coach.PricePerSession,
                GivenFeedback = false,
                CancelationUri = invitee.CancelUrl,
                ReschedulingUri = invitee.RescheduleUrl,
                EventSessionType = session.Location.Type,
                JoinSessionUri = session.Location.JoinUrl,
                Topic = session.Name
            };

            await this.liveSessionRepository.AddAsync(coachSession);
            await this.liveSessionRepository.SaveChangesAsync();

            // Coach Sendgrid Emails are disable until test/production environment to avoid spam emails

            //var subject =
            //    string.Format(
            //        GlobalConstants.coachSessionSubject,
            //        invitee.Name,
            //        session.StartTime.ToShortDateString(),
            //        session.Name);

            //var plainText = string.Empty;
            //var startTime = session.StartTime.ToShortTimeString() + " " + session.StartTime.ToLongDateString();
            //var textAsHtml =
            //    string.Format(
            //        GlobalConstants.coachSessionNotificationAsHtml,
            //    coach.FullName,
            //    session.Name,
            //    invitee.Name,
            //    invitee.Email,
            //    startTime,
            //    session.Location.JoinUrl);

            //     await emailSender.SendMailAsync(subject, coach.Email, coach.FullName, string.Empty, textAsHtml);
        }
    }
}
