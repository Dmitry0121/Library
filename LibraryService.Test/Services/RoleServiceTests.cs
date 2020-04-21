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
    public class RoleServiceTests
    {
        private Mock<IRoleRepository> _mockRepository;
        private IMapper _mapper;

        [TestInitialize]
        public void RoleService()
        {
            _mockRepository = new Mock<IRoleRepository>();
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Role, RoleDto>();
                cfg.CreateMap<RoleDto, Role>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [TestMethod]
        public void RoleService_Can_Get_Admin_Role()
        {
            #region Arrange
            var roles = GetRoles().Where(x => x.Name.Equals("Admin")).ToList();
            var expectedRole = roles.First();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Role, bool>>())).Returns(roles);
            var service = new RoleService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualRole = service.GetAdminRole();

            //Assert
            Assert.AreEqual(expectedRole.Id, actualRole.Id);
            Assert.AreEqual(expectedRole.Name, actualRole.Name);
        }

        [TestMethod]
        public void RoleService_Can_Get_Reader_Role()
        {
            #region Arrange
            var roles = GetRoles().Where(x => x.Name.Equals("Reader")).ToList();
            var expectedRole = roles.First();
            _mockRepository.Setup(m => m.Get(It.IsAny<Func<Role, bool>>())).Returns(roles);
            var service = new RoleService(_mockRepository.Object, _mapper);
            #endregion

            //Act
            var actualRole = service.GetReaderRole();

            //Assert
            Assert.AreEqual(expectedRole.Id, actualRole.Id);
            Assert.AreEqual(expectedRole.Name, actualRole.Name);
        }

        #region TestData
        private static List<Role> GetRoles()
        {
            return new List<Role>()
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
        }
        #endregion
    }
}