using AutoMapper;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using LibraryUI.Controllers;
using LibraryUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Web;
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
            HttpContext.Current = TestData.MockHttpContext();
        }

        [TestMethod]
        public void BookController_Can_View_Index()
        {
            #region Arrange
            var books = TestData.GetBooksDto();
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
            var book = TestData.GetBooksDto().Where(x => x.Id.Equals(bookId)).First();
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
            var book = TestData.GetBooksViewModel().First();
            _bookService.Setup(m => m.Give(book.Id, currentUserId));
            HttpContext.Current.Session.Add("CurrentUser", TestData.GetUserViewModel().Where(x => x.Id.Equals(currentUserId)).First());
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
            var book = TestData.GetBooksDto().Where(x => x.Id.Equals(bookId)).First();
            _bookService.Setup(m => m.Get(bookId)).Returns(book);
            var history = TestData.GetHistoryDto().Where(x => x.BookId.Equals(bookId)).ToList();
            _historyService.Setup(m => m.GetBookHistory(bookId)).Returns(history);
            var controller = new BookController(_bookService.Object, _historyService.Object, _mapper);
            #endregion

            // Act
            var result = controller.BookHistory(bookId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}