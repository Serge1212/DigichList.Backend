using DigichList.Backend.Controllers;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
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
        [Fact]
        public async Task GetDefects_Returns_The_Correct_Number_Of_Defects()
        {

            // Arrange

            var repo = new Mock<IDefectRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestDefects());
            var controller = new DefectController(repo.Object);

            // Act

            var result = await controller.GetDefects();

            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Defect>>(viewResult.Value);
            Assert.Equal(GetTestDefects().Count, model.Count());
        }
        private List<Defect> GetTestDefects()
        {
            var defects = new List<Defect>
            {
                new Defect { Id=1, UserId = 1, RoomNumber = 1 },
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
            var repo = new Mock<IDefectRepository>();
            repo.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(GetTestDefects().FirstOrDefault(a => a.Id == id));
            var controller = new DefectController(repo.Object);

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
            var repo = new Mock<IDefectRepository>();
            repo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetTestDefects());
            var controller = new DefectController(repo.Object);
            var id = 9999;

            //Act  
            var data = await controller.DeleteDefect(id);

            //Assert  
            Assert.IsType<NotFoundObjectResult>(data);
        }

    }
}
