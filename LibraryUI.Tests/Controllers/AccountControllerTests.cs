using AutoMapper;
using LibraryService.Dto;
using LibraryService.Services.Interfaces;
using LibraryUI.Controllers;
using LibraryUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryUI.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IRoleService> _roleService;
        private IMapper _mapper;

        [TestInitialize]
        public void AccountController()
        {
            _userService = new Mock<IUserService>();
            _roleService = new Mock<IRoleService>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDto, UserViewModel>();
                cfg.CreateMap<UserViewModel, UserDto>();
                cfg.CreateMap<RegisterViewModel, UserDto>();
                cfg.CreateMap<RoleDto, RoleViewModel>();
                cfg.CreateMap<RoleViewModel, RoleDto>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void AccountController_Can_View_LogIn()
        {
            #region Arrange
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            #endregion

            // Act
            var result = controller.LogIn() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccountController_Can_LogIn()
        {
            #region Arrange
            var expected = "Index";
            var user = new LoginViewModel()
            {
                Email = "JacobSophia@test.com"
            };
            var email = "JacobSophia@test.com";
            var returnUser = GetUsersDto().Where(x => x.Email.Equals(email)).First();
            _userService.Setup(m => m.GetByEmail(email)).Returns(returnUser);
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            controller.FakeHttpContext();
            #endregion

            // Act
            var result = controller.LogIn(user) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void AccountController_Can_Not_LogIn_With_Nonexistent_User()
        {
            #region Arrange
            var expected = "LogIn";
            var user = new LoginViewModel()
            {
                Email = "nonexistent@test.com"
            };
            var email = "nonexistent@test.com";
            _userService.Setup(m => m.GetByEmail(email));
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            #endregion

            // Act
            var result = controller.LogIn(user) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void AccountController_Can_View_Register()
        {
            #region Arrange
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Register() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void AccountController_Can_Register_User()
        {
            #region Arrange
            var expected = "LogIn";
            var user = new RegisterViewModel()
            {
                Email = "JacobSophia@test.com"
            };
            var role = new RoleDto()
            {
                Id = 2
            };
            _roleService.Setup(m => m.GetReaderRole()).Returns(role);
            _userService.Setup(m => m.Create(It.IsAny<UserDto>())).Returns(true);
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            #endregion

            // Act
            var result = controller.Register(user) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }

        [TestMethod]
        public void AccountController_Can_Logout()
        {
            #region Arrange
            var expected = "LogIn";
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            controller.FakeHttpContext();
            #endregion

            // Act
            var result = controller.Logout() as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
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
        #endregion
    }

    public class MockHttpSession : HttpSessionStateBase
    {
        Dictionary<string, object> Session = new Dictionary<string, object>();
        public override object this[string name]
        {
            get { return Session[name]; }
            set { Session[name] = value; }
        }
    }

    public static class FakeContext
    {
        public static HttpContextBase FakeHttpContext(this Controller controller)
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new MockHttpSession() {
                {
                    "CurrentUser",
                    new UserDto()
                    {
                        Id = 1,
                        FirstName = "Jacob",
                        LastName = "Sophia",
                        Email = "JacobSophia@test.com",
                        RoleId = 1
                    }
                },
                {
                    "FullName",
                    "Full Name"
                }
            };
            var server = new Mock<HttpServerUtilityBase>();

            context.Setup(ctx => ctx.Request).Returns(request.Object);
            context.Setup(ctx => ctx.Response).Returns(response.Object);
            context.Setup(ctx => ctx.Session).Returns(session);
            context.Setup(ctx => ctx.Server).Returns(server.Object);

            return context.Object;
        }
    }
}