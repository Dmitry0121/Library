using LibraryDataAccess.Configuration;
using LibraryDataAccess.Repositories;
using LibraryDataAccess.Repositories.Interfaces;
using Ninject.Modules;

namespace LibraryService.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument(new LibraryContext());
            Bind<IRoleRepository>().To<RoleRepository>().WithConstructorArgument(new LibraryContext());
            Bind<IAuthorRepository>().To<AuthorRepository>().WithConstructorArgument(new LibraryContext());
            Bind<IBookRepository>().To<BookRepository>().WithConstructorArgument(new LibraryContext());
            Bind<IHistoryRepository>().To<HistoryRepository>().WithConstructorArgument(new LibraryContext());
        }
    }
}