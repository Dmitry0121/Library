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
    public class HomeControllerTests
    {
        private Mock<IHistoryService> _historyService;
        private Mock<IBookService> _bookService;
        private Mock<ControllerContext> _controllerContext;
        private IMapper _mapper;

        [TestInitialize]
        public void HomeController()
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
            _controllerContext = new Mock<ControllerContext>();
            _controllerContext.SetupGet(p => p.HttpContext.Session["CurrentUser"])
                .Returns(TestData.GetUserViewModel().Where(x => x.Id.Equals(TestData.CurrentUserId)).First());
            _controllerContext.SetupGet(p => p.HttpContext.Session["FullName"])
                .Returns("Full Name");
        }

        [TestMethod]
        public void HomeController_Can_View_Index()
        {
            #region Arrange
            var expectedHistory = TestData.GetHistoryDto();
            _historyService.Setup(m => m.GetUserActiveHistory(TestData.CurrentUserId)).Returns(expectedHistory);
            var controller = new HomeController(_historyService.Object, _bookService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Can_View_Return()
        {
            #region Arrange
            var historyId = 1;
            var history = TestData.GetHistoryDto().Where(x => x.Id.Equals(historyId)).First();
            _historyService.Setup(m => m.Get(historyId)).Returns(history);
            var controller = new HomeController(_historyService.Object, _bookService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Return(historyId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_Can_Return_Book()
        {
            #region Arrange
            var expected = "Index";
            var history = TestData.GetHistoryViewModel().First();
            _bookService.Setup(m => m.Return(history.BookId, history.Id));
            var controller = new HomeController(_historyService.Object, _bookService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Return(history) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }
    }
}