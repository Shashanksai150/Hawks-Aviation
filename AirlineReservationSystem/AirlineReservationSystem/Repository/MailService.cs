#region Using Namespaces
using AirlineReservationSystem.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
#endregion

namespace AirlineReservationSystem.Repository
{
    #region Mail Service
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
        }
        #region SendEmail
        public void SendEmail(MailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Date = DateTime.Now;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            var username = _config.GetSection("EmailUsername").Value;
            var password = _config.GetSection("EmailPassword").Value;
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

            //var email = new MimeMessage();
            //email.Sender = MailboxAddress.Parse("");
            //email.To.Add(MailboxAddress.Parse(mailRequest.To));
            //email.Subject = mailRequest.Subject;
            //var builder = new BodyBuilder();
            //builder.HtmlBody = mailRequest.Body;
            //email.Body = builder.ToMessageBody();
            //using var smtp = new SmtpClient();
            //smtp.Connect("", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate("", "");
            //await smtp.SendAsync(email);
            //smtp.Disconnect(true);
        }
        #endregion
    }
#endregion
}
