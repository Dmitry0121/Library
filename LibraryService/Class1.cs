using AutoMapper;
using LibraryDataAccess.Configuration;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories;
using LibraryService.Dto;
using LibraryService.Services;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    class Program
    {
        static void Main(string[] args)
        {
            IMapper mapper;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<AuthorDto, Author>();
                cfg.CreateMap<History, HistoryDto>();
                cfg.CreateMap<HistoryDto, History>();
            });
            mapper = new Mapper(config);
            var userService = new UserService(new UserRepository(new LibraryContext()), mapper);
            var authorService = new AuthorService(new AuthorRepository(new LibraryContext()), mapper);
            var bookService = new BookService(new BookRepository(new LibraryContext()), mapper);
            var historyService = new HistoryService(new HistoryRepository(new LibraryContext()), mapper);

            //user
            var users = userService.Get();
            var user2 = userService.Get(2);
            var userEmail = userService.GetByEmail("JaydenOlivia@test.com");
            userService.Create(new UserDto()
            {
                Email = "UserService.com",
                FirstName = "UserService",
                LastName = "UserService",
                RoleId = 2
            });
            userEmail.LastName = "UserService Update";
            userService.Update(userEmail);
            userService.Delete(21);

            //author     
            var authors = authorService.Get();
            var author2 = authorService.Get(2);
            authorService.Create(new AuthorDto()
            {
                FirstName = "AuthorService",
                LastName = "AuthorService",
                Books = new List<BookDto>()
            });
            author2.LastName = "AuthorService Update 1";
            authorService.Update(author2);
            authorService.Delete(20);

            //book
            var books = bookService.Get();
            var book2 = bookService.Get(2);
            bookService.Create(new BookDto()
            {
                Count = 1,
                Name = "BookService",
                Authors = new List<AuthorDto>()
            });
            book2.Name = "BookService Update 1";
            bookService.Update(book2);
            bookService.Delete(22);    
            bookService.Give(books.First().Id, users.First().Id);
            var userHistory = historyService.GetUserHistory(1);
            bookService.Return(1, 5);

            //history
            var history = historyService.Get();
            var history1 = historyService.Get(1);
        }
    }
}
