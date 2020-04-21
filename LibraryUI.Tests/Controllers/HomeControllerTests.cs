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
    public class HomeControllerTests
    {
        private Mock<IHistoryService> _historyService;
        private Mock<IBookService> _bookService;
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
        }

        [TestMethod]
        public void HomeController_Can_View_Index()
        {
            #region Arrange
            var currentUserId = 1;
            var expectedHistory = GetHistoryDto();
            _historyService.Setup(m => m.GetUserActiveHistory(currentUserId))
                .Returns(expectedHistory);
            var controller = new HomeController(_historyService.Object,
                _bookService.Object,
                _mapper);
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
            var history = GetHistoryDto().Where(x => x.Id.Equals(historyId)).First();
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
            var history = GetHistoryViewModel().First();
            _bookService.Setup(m => m.Return(history.BookId, history.Id));
            var controller = new HomeController(_historyService.Object, _bookService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Return(history) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        #region TestData
        private static List<HistoryViewModel> GetHistoryViewModel()
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