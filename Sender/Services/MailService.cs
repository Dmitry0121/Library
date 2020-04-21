using LibrarySendEmail.Models;
using LibrarySendEmail.Services.Interfaces;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace LibrarySendEmail.Services
{
    public class MailService : IMailService
    {
        private readonly MailAddress _from;
        private readonly SmtpClient _smtp;

        public MailService()
        {
            //var email = ConfigurationManager.AppSettings.Get("email");
            //var password = ConfigurationManager.AppSettings.Get("password");
            //var name = ConfigurationManager.AppSettings.Get("name");
            //var host = ConfigurationManager.AppSettings.Get("host");
            //int.TryParse(ConfigurationManager.AppSettings.Get("port"), out int port);

            //_from = new MailAddress(email, name);
            //_smtp = new SmtpClient(host, port);
            //_smtp.Credentials = new NetworkCredential(email, password);
            //_smtp.EnableSsl = true;
        }

        public void Send(MailMessageModel mailModel)
        {
            _smtp.Send(CreateMailMessage(mailModel));
        }

        private MailMessage CreateMailMessage(MailMessageModel mailModel)
        {
            var to = new MailAddress(mailModel.To);
            var mail = new MailMessage(_from, to);
            mail.Subject = mailModel.Subject;
            mail.Body = mailModel.Body;
            return mail;
        }
    }
}