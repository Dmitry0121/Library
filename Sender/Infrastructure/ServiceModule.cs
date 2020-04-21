using LibrarySendEmail.Services;
using LibrarySendEmail.Services.Interfaces;
using Ninject.Modules;

namespace LibrarySendEmail.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailService>().To<MailService>();
        }
    }
}