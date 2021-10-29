namespace UpSkill.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using SendGrid;
    using SendGrid.Helpers.Errors.Model;
    using SendGrid.Helpers.Mail;
    using UpSkill.Services.Data.Contracts;

    public class EmailSender : IEmailSender
    {
        private readonly SendGridEmailSenderOptions options;

        public EmailSender(
            IOptions<SendGridEmailSenderOptions> options
            )
        {
            this.options = options.Value;
        }



        public async Task<Response> SendMailAsync(string subject, string toEmail, string toFullName, string message)
        {
            var client = new SendGridClient(this.options.ApiKey);
            var from = new EmailAddress(this.options.SenderEmail, this.options.SenderName);
            var to = new EmailAddress(toEmail, toFullName);

            var email = MailHelper.CreateSingleEmail(from, to, subject, message, htmlContent:"");

            //disable tracking settings ref.: https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            email.SetClickTracking(false, false);
            email.SetOpenTracking(false);
            email.SetGoogleAnalytics(false);
            email.SetSubscriptionTracking(false);

            var result = await client.SendEmailAsync(email);

            //TODO Impelment Error Handling

            return result;
        }

        public async Task<Response> SendMailAsync(string subject, string toEmail, string toFullName, string plainTextMessage, string htmlContent = "")
        {
            var client = new SendGridClient(this.options.ApiKey);
            var from = new EmailAddress(this.options.SenderEmail, this.options.SenderName);
            var to = new EmailAddress(toEmail, toFullName);

            var email = MailHelper.CreateSingleEmail(from, to, subject, plainTextMessage, htmlContent);

            //disable tracking settings ref.: https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            email.SetClickTracking(false, false);
            email.SetOpenTracking(false);
            email.SetGoogleAnalytics(false);
            email.SetSubscriptionTracking(false);

            var result = await client.SendEmailAsync(email);

            //TODO Impelment Error Handling
            // a small change to be able to commit
            return result;
        }
    }
}
