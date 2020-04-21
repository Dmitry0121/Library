using AutoMapper;
using LibraryDataAccess.Entities;
using LibraryDataAccess.Repositories.Interfaces;
using LibraryService.Dto;
using LibraryService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Test.Services
{
    [TestClass]
    public class HistoryServiceTests
    {
        private Mock<IHistoryRepository> _mockRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void HistoryService()
        {
            _mockRepository = new Mock<IHistoryRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<History, HistoryDto>();
                cfg.CreateMap<HistoryDto, History>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void HistoryService_Can_Get_All_Items()
        {
            #region Arrange
            var expectedHistory = GetHistory();
            _mockRepository.Setup(m => m.Get(b => b.Book, u => u.User)).Returns(expectedHistory);
            var service = new HistoryService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualHistory = service.Get();

            //Assert
            Assert.AreEqual(expectedHistory.Count(), actualHistory.Count());
        }

        [TestMethod]
        public void HistoryService_Can_Get_By_Id()
        {
            #region Arrange
            var id = 2;
            var history = GetHistory().Where(x => x.Id.Equals(id)).ToList();
            var expectedHistory = GetHistory().Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<History, bool>>(), b => b.Book, u => u.User)).Returns(history);
            var service = new HistoryService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualHistory = service.Get(id);

            //Assert
            Assert.AreEqual(expectedHistory.Id, actualHistory.Id);
            Assert.AreEqual(expectedHistory.BookId, actualHistory.BookId);
            Assert.AreEqual(expectedHistory.UserId, actualHistory.UserId);
            Assert.AreEqual(expectedHistory.ReturnDate, actualHistory.ReturnDate);
        }

        [TestMethod]
        public void HistoryService_Can_Get_User_History()
        {
            #region Arrange
            var id = 1;
            var expectedHistory = GetHistory().Where(x => x.UserId.Equals(id)).ToList();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<History, bool>>(), b => b.Book, u => u.User)).Returns(expectedHistory);
            var service = new HistoryService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualHistory = service.GetUserHistory(id);

            //Assert
            Assert.AreEqual(expectedHistory.Count(), actualHistory.Count());
        }

        #region TestData
        private static List<History> GetHistory()
        {
            return new List<History>()
            {
                new History()
                {
                    Id = 1,
                    DateReceiving = DateTime.Now.AddDays(-2),
                    ReturnDate = null,
                    BookId = 1,
                    UserId = 1
                },
                new History()
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