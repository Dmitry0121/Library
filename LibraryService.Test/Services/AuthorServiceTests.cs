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
    public class AuthorServiceTests
    {
        private Mock<IAuthorRepository> _mockRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void AuthorService()
        {
            _mockRepository = new Mock<IAuthorRepository>();
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
        public void AuthorService_Can_Get_All_Items()
        {
            #region Arrange
            var expectedAuthors = GetAuthors();
            _mockRepository.Setup(m => m.Get(b => b.Books)).Returns(expectedAuthors);
            var service = new AuthorService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualAuthors = service.Get();

            //Assert
            Assert.AreEqual(expectedAuthors.Count(), actualAuthors.Count());
        }

        [TestMethod]
        public void AuthorService_Can_Get_By_Id()
        {
            #region Arrange
            var id = 3;
            var authors = GetAuthors().Where(x => x.Id.Equals(id)).ToList();
            var expectedAuthor = GetAuthors().Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Author, bool>>(), b => b.Books)).Returns(authors);
            var service = new AuthorService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualAuthor = service.Get(id);

            //Assert
            Assert.AreEqual(expectedAuthor.Id, actualAuthor.Id);
            Assert.AreEqual(expectedAuthor.FirstName, actualAuthor.FirstName);
            Assert.AreEqual(expectedAuthor.LastName, actualAuthor.LastName);
            Assert.AreEqual(expectedAuthor.Books.Count(), actualAuthor.Books.Count());
        }

        [TestMethod]
        public void AuthorService_Can_Create()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Create(It.IsAny<Author>())).Returns(true);
            var service = new AuthorService(_mockRepository.Object, _mapper);
            var author = GetAuthorsDto().First();
            #endregion

            //Act
            var result = service.Create(author);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthorService_Can_Update()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Update(It.IsAny<Author>())).Returns(true);
            var service = new AuthorService(_mockRepository.Object, _mapper);
            var user = GetAuthorsDto().First();
            user.FirstName = "Update";
            #endregion

            //Act
            var result = service.Update(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AuthorService_Can_Delete()
        {
            #region Arrange
            var id = 1;
            _mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(true);
            var service = new AuthorService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Delete(id);

            // Assert
            Assert.IsTrue(result);
        }

        #region TestData
        private static List<AuthorDto> GetAuthorsDto()
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

            return authors;
        }
        private static List<Author> GetAuthors()
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

            return authors;
        }
        #endregion
    }
}