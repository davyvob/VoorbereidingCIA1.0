using Howest.Prog.CoinChop.Core.Common;
using Howest.Prog.CoinChop.Core.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Howest.Prog.CoinChop.Infrastructure.Email
{
    public class SmtpMailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public SmtpMailService(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public void SendAddedToGroupMail(string recipientName, string recipientEmail, TokenMailTemplateData templateData) 
        {
            string groupUrl = $"{_mailSettings.PublicSiteUrl}/group/{templateData.Token}";
            string title = "You're invited to a new CoinChop group";
            string body =
$@"Hi {recipientName},<br />
<br />
<p>Someone from the CoinChop group ""{templateData.GroupName}"" added you to the group so you can split your shared expenses.</p>
<p>Please visit <a href=""{groupUrl}"">{groupUrl}</a> to view your shared dashboard and start adding your expenses!</p>
<br />
<p>Kindly<br />The CoinChop Team</p>
";
            SendMail(new MailAddress(recipientEmail, recipientName), title, body);
        }

        public void SendMail(MailAddress recipient, string title, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_mailSettings.FromAddress, _mailSettings.FromName);
            mail.To.Add(recipient);

            mail.Subject = title;
            mail.Body = body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient(_mailSettings.Smtp.Host);
            client.Port = _mailSettings.Smtp.SmtpPort;
            client.Credentials = new NetworkCredential(_mailSettings.Smtp.Username, _mailSettings.Smtp.Password);
            client.EnableSsl = _mailSettings.Smtp.UseTLS;

            client.Send(mail);
        }
    }
}
