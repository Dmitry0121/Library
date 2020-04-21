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
    public class BookServiceTests
    {
        private Mock<IBookRepository> _mockRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void BookService()
        {
            _mockRepository = new Mock<IBookRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Author, AuthorDto>();
                cfg.CreateMap<AuthorDto, Author>();
                cfg.CreateMap<Book, BookDto>();
                cfg.CreateMap<BookDto, Book>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void BookService_Can_Get_All_Items()
        {
            #region Arrange
            var expectedBooks = GetBooks();
            _mockRepository.Setup(m => m.Get(a => a.Authors)).Returns(expectedBooks);
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualBooks = service.Get();

            //Assert
            Assert.AreEqual(expectedBooks.Count(), actualBooks.Count());
        }

        [TestMethod]
        public void BookService_Can_Get_By_Id()
        {
            #region Arrange
            var id = 1;
            var books = GetBooks().Where(x => x.Id.Equals(id)).ToList();
            var expectedBook = GetBooks().Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Book, bool>>(), a => a.Authors)).Returns(books);
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualBook = service.Get(id);

            //Assert
            Assert.AreEqual(expectedBook.Id, actualBook.Id);
            Assert.AreEqual(expectedBook.Name, actualBook.Name);
            Assert.AreEqual(expectedBook.Count, actualBook.Count);
            Assert.AreEqual(expectedBook.Authors.Count(), actualBook.Authors.Count());
        }

        [TestMethod]
        public void BookService_Can_Give_Book()
        {
            #region Arrange
            var bookId = 1;
            var userId = 1;
            var books = GetBooks().Where(x => x.Id.Equals(bookId)).ToList();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Book, bool>>(), a => a.Authors)).Returns(books);
            _mockRepository.Setup(m => m.Give(It.IsAny<Book>(), It.IsAny<History>())).Returns(true);
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Give(bookId, userId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookService_Can_Not_Give_Nonexistent_Book()
        {
            #region Arrange
            var bookId = -1;
            var userId = 1;
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Book, bool>>(), a => a.Authors)).Returns(new List<Book>());
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Give(bookId, userId);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookService_Can_Return_Book()
        {
            #region Arrange
            var bookId = 1;
            var historyId = 1;
            var books = GetBooks().Where(x => x.Id.Equals(bookId)).ToList();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Book, bool>>(), a => a.Authors)).Returns(books);
            _mockRepository.Setup(m => m.Return(It.IsAny<Book>(), It.IsAny<History>())).Returns(true);
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Return(bookId, historyId);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookService_Can_Not_Return_Nonexistent_Book()
        {
            #region Arrange
            var bookId = 1;
            var historyId = 1;
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Book, bool>>(), a => a.Authors)).Returns(new List<Book>());
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Return(bookId, historyId);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BookService_Can_Create()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Create(It.IsAny<Book>())).Returns(true);
            var service = new BookService(_mockRepository.Object, _mapper);
            var book = GetBooksDto().First();
            #endregion

            //Act
            var result = service.Create(book);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookService_Can_Update()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Update(It.IsAny<Book>())).Returns(true);
            var service = new BookService(_mockRepository.Object, _mapper);
            var book = GetBooksDto().First();
            book.Name = "Update";
            #endregion

            //Act
            var result = service.Update(book);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void BookService_Can_Delete()
        {
            #region Arrange
            var id = 1;
            _mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(true);
            var service = new BookService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Delete(id);

            //Assert
            Assert.IsTrue(result);
        }

        #region TestData
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
        private static List<Book> GetBooks()
        {
            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    FirstName = "Alexander",
                    LastName = "Lloyd",
                    Books = new List<Book>()
                },
                new Author()
                {
                    Id = 2,
                    FirstName = "Anders",
                    LastName = "Jane",
                    Books = new List<Book>()
                },
                new Author()
                {
                    Id = 3,
                    FirstName = "Jacob",
                    LastName = "Sophia",
                    Books = new List<Book>()
                },
            };
            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Count = 50,
                    Name = "The Book of Three",
                    Authors = new List<Author>()
                },
                new Book()
                {
                    Id = 2,
                    Count = 2,
                    Name = "The Black Cauldron",
                    Authors = new List<Author>()
                },
            };

            books[0].Authors.Add(authors[0]);
            books[0].Authors.Add(authors[1]);
            books[1].Authors.Add(authors[1]);
            books[1].Authors.Add(authors[2]);

            authors[0].Books.Add(books[0]);
            authors[1].Books.Add(books[0]);
            authors[1].Books.Add(books[1]);
            authors[2].Books.Add(books[1]);

            return books;
        }
        #endregion
    }
}