using AutoMapper;
using DigichList.Backend.Controllers;
using DigichList.Backend.Mappers;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DigicnList.Backend.Tests
{
    public class DefectControllerTests
    {

        private static IMapper _mapper;
        private Mock<IDefectRepository> _repo;
        private Mock<IUserRepository> _userRepo;
        private Mock<IBotNotificationSender> _iBotNotificationSender;

        public DefectControllerTests()
        {
            _repo = new Mock<IDefectRepository>();
            _userRepo = new Mock<IUserRepository>();
            _iBotNotificationSender = new Mock<IBotNotificationSender>();
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DefectMapperProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestDefects());
        }

        [Fact]
        public  void GetDefects_Returns_The_Correct_Number_Of_Defects()
        {

            // Arrange

            var controller = new DefectController(_repo.Object, _userRepo.Object, _iBotNotificationSender.Object, _mapper);

            // Act

            var result = controller.GetDefects();

            // Assert

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<DefectViewModel>>(viewResult.Value);
            Assert.Equal(GetTestDefects().Count, model.Count());
        }
        private List<Defect> GetTestDefects()
        {
            var defects = new List<Defect>
            {
                new Defect { Id=1, UserId = 1, RoomNumber = 1, Description = "first"},
                new Defect { Id=2, UserId = 2, RoomNumber = 2 },
                new Defect { Id=3, UserId = 3, RoomNumber = 3 },
            };
            return defects;
        }
        [Fact]
        public async Task GetDefect_Returns_Correct_Defect()
        {
            // Arrange

            int id = 2;
            var controller = new DefectController(_repo.Object, _userRepo.Object, _iBotNotificationSender.Object, _mapper);

            // Act

            var result = await controller.GetDefect(id);

            //Assert

            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Defect>(viewResult.Value);

            Assert.Equal(id, model.Id);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundObjectResult()
        {
            //Arrange  

            var controller = new DefectController(_repo.Object, _userRepo.Object, _iBotNotificationSender.Object, _mapper);
            var id = 9999;

            //Act  

            var data = await controller.DeleteDefect(id);

            //Assert  

            Assert.IsType<NotFoundObjectResult>(data);
        }

    }
}
