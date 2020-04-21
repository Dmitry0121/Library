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
    public class UserServiceTests
    {
        private Mock<IUserRepository> _mockRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void UserService()
        {
            _mockRepository = new Mock<IUserRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void UserService_Can_Get_All_Items()
        {
            #region Arrange
            var expectedUsers = GetUsers();
            _mockRepository.Setup(m => m.Get(r => r.Role)).Returns(expectedUsers);
            var service = new UserService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualUsers = service.Get();

            //Assert
            Assert.AreEqual(expectedUsers.Count(), actualUsers.Count());
        }

        [TestMethod]
        public void UserService_Can_Get_By_Id()
        {
            #region Arrange
            var id = 3;
            var users = GetUsers().Where(x => x.Id.Equals(id)).ToList();
            var expectedUser = GetUsers().Where(x => x.Id.Equals(id)).ToList().FirstOrDefault();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<User, bool>>(), r => r.Role)).Returns(users);
            var service = new UserService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualUser = service.Get(id);

            //Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
        }

        [TestMethod]
        public void UserService_Can_Get_By_Email()
        {
            #region Arrange
            var email = "JacobSophia@test.com";
            var expectedUsers = GetUsers().Where(x => x.Email.Equals(email)).ToList();
            var expectedUser = GetUsers().Where(x => x.Email.Equals(email)).ToList().FirstOrDefault();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<User, bool>>(), r => r.Role)).Returns(expectedUsers);
            var service = new UserService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualUser = service.GetByEmail(email);

            //Assert
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
            Assert.AreEqual(expectedUser.Email, actualUser.Email);
            Assert.AreEqual(expectedUser.FirstName, actualUser.FirstName);
            Assert.AreEqual(expectedUser.LastName, actualUser.LastName);
        }

        [TestMethod]
        public void UserService_Can_Create()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Create(It.IsAny<User>())).Returns(true);
            var service = new UserService(_mockRepository.Object, _mapper);
            var user = GetUsersDto().First();
            #endregion

            //Act
            var result = service.Create(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserService_Can_Update()
        {
            #region Arrange
            _mockRepository.Setup(m => m.Update(It.IsAny<User>())).Returns(true);
            var service = new UserService(_mockRepository.Object, _mapper);
            var user = GetUsersDto().First();
            user.FirstName = "Update";
            #endregion

            //Act
            var result = service.Update(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UserService_Can_Delete()
        {
            #region Arrange
            var id = 3;
            _mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(true);
            var service = new UserService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var result = service.Delete(id);

            // Assert
            Assert.IsTrue(result);
        }

        #region TestData
        private static List<UserDto> GetUsersDto()
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
        private static List<User> GetUsers()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Reader"
                }
            };
            var users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Jacob",
                    LastName = "Sophia",
                    Email = "JacobSophia@test.com",
                    RoleId = 1,
                    Role = roles[0]
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Mason",
                    LastName = "Isabella",
                    Email = "MasonIsabella@test.com",
                    RoleId = 1,
                    Role = roles[0]
                },
                new User()
                {
                    Id = 3,
                    FirstName = "William",
                    LastName = "Emma",
                    Email = "WilliamEmma@test.com",
                    RoleId = 1,
                    Role = roles[0]
                },
                new User()
                {
                    Id = 4,
                    FirstName = "Jayden",
                    LastName = "Olivia",
                    Email = "JaydenOlivia@test.com",
                    RoleId = 2,
                    Role = roles[1]
                },
            };
            return users;
        }
        #endregion
    }
}