using AutoMapper;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using LibraryUI.Controllers;
using LibraryUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LibraryUI.Tests.Controllers
{
    [TestClass]
    public class BookControllerTests
    {
        private Mock<IBookService> _bookService;
        private Mock<IHistoryService> _historyService;        
        private IMapper _mapper;

        [TestInitialize]
        public void BookController()
        {
            _historyService = new Mock<IHistoryService>();
            _bookService = new Mock<IBookService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HistoryDto, HistoryViewModel>();
                cfg.CreateMap<HistoryViewModel, HistoryDto>();
                cfg.CreateMap<BookDto, BookViewModel>();
                cfg.CreateMap<BookViewModel, BookDto>();
                cfg.CreateMap<AuthorDto, AuthorViewModel>();
                cfg.CreateMap<AuthorViewModel, AuthorDto>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void BookController_Can_View_Index()
        {
            #region Arrange
            var books = GetBooksDto();
            _bookService.Setup(m => m.Get()).Returns(books);
            var controller = new BookController(_bookService.Object, _historyService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BookController_Can_View_Give()
        {
            #region Arrange
            var bookId = 1;
            var book = GetBooksDto().Where(x=>x.Id.Equals(bookId)).First();
            _bookService.Setup(m => m.Get(bookId)).Returns(book);
            var controller = new BookController(_bookService.Object, _historyService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Give(bookId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BookController_Can_Give_Book()
        {
            #region Arrange
            var currentUserId = 1;
            var expected = "Index";
            var book = GetBooksViewModel().First();
            _bookService.Setup(m => m.Give(book.Id, currentUserId));
            var controller = new BookController(_bookService.Object, _historyService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Give(book) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void BookController_Can_View_BookHistory()
        {
            #region Arrange
            var bookId = 1;
            var book = GetBooksDto().Where(x => x.Id.Equals(bookId)).First();
            _bookService.Setup(m => m.Get(bookId)).Returns(book);
            var history = GetHistoryDto().Where(x => x.BookId.Equals(bookId)).ToList();
            _historyService.Setup(m => m.GetBookHistory(bookId)).Returns(history);
            var controller = new BookController(_bookService.Object, _historyService.Object, _mapper);
            #endregion

            // Act
            var result = controller.BookHistory(bookId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        #region TestData
        private static List<BookViewModel> GetBooksViewModel()
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
        private static List<BookDto> GetBooksDto()
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
        private static List<HistoryDto> GetHistoryDto()
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
        #endregion
    }
}