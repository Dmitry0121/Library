using LibrarySendEmail.Models;

namespace LibrarySendEmail.Services.Interfaces
{
    public interface IMailService
    {
        void Send(MailMessageModel mailModel);
    }
}