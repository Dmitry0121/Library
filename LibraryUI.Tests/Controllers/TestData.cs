using LibraryService.Dto;
using LibraryUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace LibraryUI.Tests.Controllers
{
    internal class TestData
    {
        internal const int CurrentUserId = 1;
        internal static HttpContext MockHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://localhost/", "");
            var httpResponce = new HttpResponse(new StringWriter());
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(), new HttpStaticObjectsCollection(), 10, true, HttpCookieMode.AutoDetect, SessionStateMode.InProc, false);
            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, CallingConventions.Standard, new[] { typeof(HttpSessionStateContainer) }, null).Invoke(new object[] { sessionContainer });
            return httpContext;
        }
        internal static List<UserViewModel> GetUserViewModel()
        {
            return new List<UserViewModel>()
            {
                new UserViewModel()
                {
                    Id = 1,
                    Email = "test@test.com",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    RoleId = 1,
                    Role = new RoleViewModel()
                    {
                        Id = 1,
                        Name = "Admin"
                    }
                }
            };
        }
        internal static List<UserDto> GetUsersDto()
        {
            return new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    FirstName = "Jacob",
                    LastName = "Sophia",
                    Email = "JacobSophia@test.com",
                    RoleId = 1
                },
                new UserDto()
                {
                    Id = 2,
                    FirstName = "Mason",
                    LastName = "Isabella",
                    Email = "MasonIsabella@test.com",
                    RoleId = 2
                },
            };
        }
        internal static List<BookViewModel> GetBooksViewModel()
        {
            var authors = new List<AuthorViewModel>()
            {
                new AuthorViewModel()
                {
                    Id = 1,
                    FirstName = "Alexander",
                    LastName = "Lloyd",
                    Books = new List<BookViewModel>()
                },
                new AuthorViewModel()
                {
                    Id = 2,
                    FirstName = "Anders",
                    LastName = "Jane",
                    Books = new List<BookViewModel>()
                }
            };
            var books = new List<BookViewModel>()
            {
                new BookViewModel()
                {
                    Id = 1,
                    Count = 50,
                    Name = "The Book of Three",
                    Authors = new List<AuthorViewModel>()
                }
            };

            books[0].Authors.Add(authors[0]);
            books[0].Authors.Add(authors[1]);
            authors[0].Books.Add(books[0]);
            authors[1].Books.Add(books[0]);

            return books;
        }
        internal static List<BookDto> GetBooksDto()
        {
            var authors = new List<AuthorDto>()
            {
                new AuthorDto()
                {
                    Id = 1,
                    FirstName = "Alexander",
                    LastName = "Lloyd",
                    Books = new List<BookDto>()
                },
                new AuthorDto()
                {
                    Id = 2,
                    FirstName = "Anders",
                    LastName = "Jane",
                    Books = new List<BookDto>()
                }
            };
            var books = new List<BookDto>()
            {
                new BookDto()
                {
                    Id = 1,
                    Count = 50,
                    Name = "The Book of Three",
                    Authors = new List<AuthorDto>()
                }
            };

            books[0].Authors.Add(authors[0]);
            books[0].Authors.Add(authors[1]);
            authors[0].Books.Add(books[0]);
            authors[1].Books.Add(books[0]);

            return books;
        }
        internal static List<HistoryDto> GetHistoryDto()
        {
            return new List<HistoryDto>()
            {
                new HistoryDto()
                {
                    Id = 1,
                    DateReceiving = DateTime.Now.AddDays(-2),
                    ReturnDate = null,
                    BookId = 1,
                    UserId = 1
                },
                new HistoryDto()
                {
                    Id = 2,
                    DateReceiving = DateTime.Now.AddDays(-3),
                    ReturnDate = null,
                    BookId = 2,
                    UserId = 1
                }
            };
        }
        internal static List<HistoryViewModel> GetHistoryViewModel()
        {
            return new List<HistoryViewModel>()
            {
                new HistoryViewModel()
                {
                    Id = 1,
                    DateReceiving = DateTime.Now.AddDays(-2),
                    ReturnDate = null,
                    BookId = 1,
                    UserId = 1
                },
                new HistoryViewModel()
                {
                    Id = 2,
                    DateReceiving = DateTime.Now.AddDays(-3),
                    ReturnDate = null,
                    BookId = 2,
                    UserId = 1
                }
            };
        }
    }
}