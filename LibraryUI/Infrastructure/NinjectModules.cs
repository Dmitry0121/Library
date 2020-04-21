using AutoMapper;
using LibraryDataAccess.Entities;
using LibraryService.Dto;
using LibraryService.Services;
using LibraryService.Services.Interfaces;
using LibraryUI.Models;
using Ninject;
using Ninject.Modules;

namespace LibraryUI.Infrastructure
{
    public class NinjectModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IAuthorService>().To<AuthorService>();
            Bind<IBookService>().To<BookService>();
            Bind<IHistoryService>().To<HistoryService>();

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }
        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, UserViewModel>();
                cfg.CreateMap<UserViewModel, UserDto>();
                cfg.CreateMap<RegisterViewModel, UserDto>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<RoleDto, RoleViewModel>();
                cfg.CreateMap<RoleViewModel, RoleDto>();
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();
                cfg.CreateMap<BookDto, BookViewModel>();
                cfg.CreateMap<BookViewModel, BookDto>();
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<AuthorDto, Author>();
                cfg.CreateMap<AuthorDto, AuthorViewModel>();
                cfg.CreateMap<AuthorViewModel, AuthorDto>();
                cfg.CreateMap<History, HistoryDto>();
                cfg.CreateMap<HistoryDto, History>();
                cfg.CreateMap<HistoryDto, HistoryViewModel>();
                cfg.CreateMap<HistoryViewModel, HistoryDto>();
            });
            return config;
        }
    }
}