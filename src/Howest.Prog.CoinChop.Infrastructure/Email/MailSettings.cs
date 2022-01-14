namespace Howest.Prog.CoinChop.Infrastructure.Email
{
    public class MailSettings
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }
        public string PublicSiteUrl { get; set; }
        public SmtpSettings Smtp { get; set; }

        public class SmtpSettings
        {
            public string Host { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int SmtpPort { get; set; }
            public bool UseTLS { get; set; }
        }

    }
}
