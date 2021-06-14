using DigichList.Backend.Controllers;
using DigichList.Backend.Helpers;
using DigichList.Backend.Options;
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
        [Fact]
        public async Task GetAdmins_Returns_The_Correct_Number_Of_Admins()
        {

            // Arrange

            var repo = new Mock<IAdminRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAdmins());
            var authOption = new Mock<IOptions<AuthOptions>>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object,authOption.Object,jwtService.Object);

            // Act

            var result = await controller.GetAdmins();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Admin>>(viewResult.Value);
            Assert.Equal(GetTestAdmins().Count, model.Count());
        }
        private List<Admin> GetTestAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin { Id=1, Username="admin1", Email = "admin1@gmail.com", AccessLevel = Admin.AccessLevels.Admin },
                new Admin { Id=2, Username="admin2", Email = "admin2@gmail.com", AccessLevel = Admin.AccessLevels.SuperAdmin },
                new Admin { Id=3, Username="admin3", Email = "admin3@gmail.com", AccessLevel = Admin.AccessLevels.Admin },
            };
            return admins;
        }
        [Fact]
        public async Task GetAdmin_Returns_Correct_Admin()
        {
            // Arrange

            int id = 2;
            var repo = new Mock<IAdminRepository>();
            repo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetTestAdmins().FirstOrDefault(a => a.Id == id));
            var authOption = new Mock<IOptions<AuthOptions>>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object, authOption.Object, jwtService.Object);

            // Act

            var result = await controller.GetAdmin(id);

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Admin>(viewResult.Value);

            Assert.Equal(id, model.Id);
        }

        [Fact]  
        public async Task Task_Add_ValidData_Return_CreatedAtActionResult()
        {
            //Arrange  
            var repo = new Mock<IAdminRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestAdmins());
            var authOption = new Mock<IOptions<AuthOptions>>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object, authOption.Object, jwtService.Object);
            var admin = new Admin() { Username = "admin11", Email = "admin11@gmail.com", AccessLevel = Admin.AccessLevels.Admin, Password = "1111" };

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
            var authOption = new Mock<IOptions<AuthOptions>>();
            var jwtService = new Mock<JwtService>();
            var controller = new AdminController(repo.Object, authOption.Object, jwtService.Object);
            var id = 9999;

            //Act  
            var data = await controller.DeleteAdmin(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }
    }
}
