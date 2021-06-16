using AutoMapper;
using DigichList.Backend.Controllers;
using DigichList.Backend.Helpers;
using DigichList.Backend.Mappers;
using DigichList.Backend.Options;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DigicnList.Backend.Tests
{
    public class RolersControllerTest
    {

        private static IMapper _mapper;
        private Mock<IRoleRepository> _repo;
        private Mock<IBotNotificationSender> _iBotNotificationSender;
        private Mock<IUserRepository> _userRepo;

        public RolersControllerTest()
        {
            _repo = new Mock<IRoleRepository>();
            _iBotNotificationSender = new Mock<IBotNotificationSender>();
            _userRepo = new Mock<IUserRepository>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new RoleMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestRoles());
        }

        [Fact]
        public async Task GetRoles_Returns_The_Correct_Number_Of_Roles()
        {

            // Arrange
            var controller = new RolesController(_repo.Object, _userRepo.Object, _mapper, _iBotNotificationSender.Object );

            // Act
            var result = await controller.GetRoles();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<RoleViewModel>>(viewResult.Value);
            Assert.Equal(GetTestRoles().Count, model.Count());
        }
        private List<Role> GetTestRoles()
        {
            var admins = new List<Role>
            {
                new Role { Id=1, Name = "first", CanFixDefects = true },
                new Role { Id=2, Name = "second", CanFixDefects = false },
                new Role { Id=3, Name = "third", CanFixDefects = true },
            };
            return admins;
        }
        [Fact]
        public async Task GetRole_Returns_Correct_Role()
        {
            // Arrange
            int id = 2;
           
            _repo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetTestRoles().FirstOrDefault(a => a.Id == id));
            var controller = new RolesController(_repo.Object, _userRepo.Object, _mapper, _iBotNotificationSender.Object);
            // Act

            var result = await controller.GetRole(id);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<ExtendedRoleViewModel>(viewResult.Value);

            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async Task Task_Add_ValidData_Return_CreatedAtActionResult()
        {
            //Arrange  
            var controller = new RolesController(_repo.Object, _userRepo.Object, _mapper, _iBotNotificationSender.Object);
            var role = new Role() { Name = "role", CanFixDefects = true };

            //Act  
            var data = await controller.CreateRole(role);

            //Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundObjectResult()
        {
            //Arrange  
            var controller = new RolesController(_repo.Object, _userRepo.Object, _mapper, _iBotNotificationSender.Object);
            var id = 9999;

            //Act  
            var data = await controller.DeleteRole(id);

            //Assert
            Assert.IsType<NotFoundObjectResult>(data);
        }

    }
}
