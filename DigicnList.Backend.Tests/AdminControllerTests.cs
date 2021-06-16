using AutoMapper;
using DigichList.Backend.Controllers;
using DigichList.Backend.Helpers;
using DigichList.Backend.Mappers;
using DigichList.Backend.Options;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DigicnList.Backend.Tests
{
    public class AdminControllerTests
    {
        private static IMapper _mapper;
        private Mock<IAdminRepository> _repo;
        private Mock<JwtService> _jwtService;

        public AdminControllerTests()
        {
            _repo = new Mock<IAdminRepository>();
            _jwtService = new Mock<JwtService>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AdminMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAdmins());
        }

        [Fact]
        public async Task GetAdmins_Returns_The_Correct_Number_Of_Admins()
        {

            // Arrange
            var controller = new AdminController(_repo.Object, _jwtService.Object, _mapper);

            // Act

            var result = await controller.GetAdmins();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<AdminViewModel>>(viewResult.Value);
            Assert.Equal(GetTestAdmins().Count, model.Count());
        }

        private List<Admin> GetTestAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin { Id=1, FirstName="admin1", Email = "admin1@gmail.com", AccessLevel = Admin.AccessLevels.Admin },
                new Admin { Id=2, FirstName="admin2", Email = "admin2@gmail.com", AccessLevel = Admin.AccessLevels.SuperAdmin },
                new Admin { Id=3, FirstName="admin3", Email = "admin3@gmail.com", AccessLevel = Admin.AccessLevels.Admin },
            };
            return admins;
        }
        [Fact]
        public async Task GetAdmin_Returns_Correct_Admin()
        {
            // Arrange

            int id = 2;
            _repo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetTestAdmins().FirstOrDefault(a => a.Id == id));
            var controller = new AdminController(_repo.Object, _jwtService.Object, _mapper);

            // Act

            var result = await controller.GetAdmin(id);

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<AdminViewModel>(viewResult.Value);

            Assert.Equal(id, model.Id);
        }



        [Fact]  
        public async Task Task_Add_ValidData_Return_CreatedAtActionResult()
        {
            //Arrange  
            var repo = new Mock<IAdminRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAdmins());
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object, jwtService.Object, mapper.Object);
            var admin = new Admin() { FirstName = "admin11", Email = "admin11@gmail.com", AccessLevel = Admin.AccessLevels.Admin, Password = "1111" };

            //Act  
            var data = await controller.CreateAdmin(admin);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundObjectResult()
        {
            //Arrange  
            var repo = new Mock<IAdminRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAdmins());
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object, jwtService.Object, mapper.Object);
            var id = 9999;

            //Act  
            var data = await controller.DeleteAdmin(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }
    }
}
