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
    public class AccountControllerTests
    {
        private Mock<IUserService> _userService;
        private Mock<IRoleService> _roleService;
        private Mock<ControllerContext> _controllerContext;
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
            HttpContext.Current = TestData.MockHttpContext();
            _controllerContext = new Mock<ControllerContext>();
            _controllerContext.SetupGet(p => p.HttpContext.Session["CurrentUser"])
                .Returns(TestData.GetUserViewModel().Where(x => x.Id.Equals(TestData.CurrentUserId)).First());
            _controllerContext.SetupGet(p => p.HttpContext.Session["FullName"])
                .Returns("Full Name");
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
            var returnUser = TestData.GetUsersDto().Where(x => x.Email.Equals(user.Email)).First();
            _userService.Setup(m => m.GetByEmail(user.Email)).Returns(returnUser);
            var controller = new AccountController(_userService.Object, _roleService.Object, _mapper);
            controller.ControllerContext = _controllerContext.Object;
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
            _userService.Setup(m => m.GetByEmail(user.Email));
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
            controller.ControllerContext = _controllerContext.Object;
            #endregion

            // Act
            var result = controller.Logout() as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.RouteValues["action"]);
        }
    }
}