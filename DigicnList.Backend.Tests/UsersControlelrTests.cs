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
    public class UsersControlelrTests
    {
        [Fact]
        public async Task GetUsers_Returns_The_Correct_Number_Of_Users()
        {

            // Arrange

            var repo = new Mock<IUserRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestUsers());
            var controller = new UsersController(repo.Object);

            // Act

            var result = await controller.GetUsers();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Value);
            Assert.Equal(GetTestUsers().Count, model.Count());
        }
        private List<User> GetTestUsers()
        {
            var admins = new List<User>
            {
                new User { Id=1, FirstName = "first", RoleId = 1 },
                new User { Id=2, FirstName = "second", RoleId = 2 },
                new User { Id=3, FirstName = "third", RoleId = 3  },
            };
            return admins;
        }
        [Fact]
        public async Task GetUser_Returns_Correct_User()
        {
            // Arrange

            int id = 2;
            var repo = new Mock<IUserRepository>();
            repo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetTestUsers().FirstOrDefault(a => a.Id == id));
            var authOption = new Mock<IOptions<AuthOptions>>();
            var jwtService = new Mock<JwtService>();
            var controller = new UsersController(repo.Object);

            // Act

            var result = await controller.GetUser(id);

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<User>(viewResult.Value);

            Assert.Equal(id, model.Id);
        }
        [Fact]
        public async Task Task_Add_ValidData_Return_CreatedAtActionResult()
        {
            //Arrange  
            var repo = new Mock<IUserRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestUsers());
            var controller = new UsersController(repo.Object);
            var user = new User() { FirstName = "user", RoleId = 1 };

            //Act  
            var data = await controller.CreateUser(user);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }

       

    }
}
