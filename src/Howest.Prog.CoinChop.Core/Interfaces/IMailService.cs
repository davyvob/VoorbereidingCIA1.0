using Howest.Prog.CoinChop.Core.Common;
using System.Net.Mail;

namespace Howest.Prog.CoinChop.Core.Interfaces
{
    public interface IMailService
    {
        void SendAddedToGroupMail(string recipientName, string recipientEmail, TokenMailTemplateData templateData);

        void SendMail(MailAddress recipient, string title, string body);

    }
}
