using Bogus.DataSets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTravelAgency.Controllers;
using TimeTravelAgency.Domain.Entity;
using TimeTravelAgency.Domain.Enum;
using TimeTravelAgency.Domain.Response;
using TimeTravelAgency.Service.Interfaces;
using Xunit;

namespace TimeTravelAgency.Tests
{
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPictureService _pictureService;

        [Fact]
        public void AboutTest()
        {
            // Arrange
            HomeController controller = new HomeController(_logger, _pictureService);

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("About", result?.ViewName);
        }

        [Fact]
        public async void IndexReturnsAViewResultWithAListOfPictures()
        {
            // Arrange
            var mock = new Mock<IPictureService>();
            mock.Setup(pics => pics.GetPictures(ViewName.Index)).Returns(GetTestPictures());
            var controller = new HomeController(_logger, mock.Object);

            // Act
            //var result = await controller.Index();
            var result = await controller.Index() as ViewResult;

            // Assert
            //var viewResult = Assert.IsType<Task<IActionResult>>(result);
            var model = Assert.IsAssignableFrom<List<Picture>>(result.Model);
            Assert.Equal(GetTestPictures().Result.Data.Count, model.Count());
        }
        private async Task<IBaseResponse<List<Picture>>> GetTestPictures()
        {
            IBaseResponse<List<Picture>> baseResponse = new BaseResponse<List<Picture>>()
            {
                Data = new List<Picture> {
                    new Picture{
                        Id = 1,
                        ViewName = ViewName.Index,
                        Title = "Index_1",
                    },
                    new Picture{
                        Id = 2,
                        ViewName = ViewName.Index,
                        Title = "Index_2",
                    },
                }
            };
            return baseResponse;
        }

        [Fact]
        public void CreatePictureReturnsRedirectError()
        {
            // Arrange
            var mock = new Mock<IPictureService>();
            var controller = new HomeController(_logger, mock.Object);
            controller.ModelState.AddModelError("File", "File is required");

            var formFile = new Mock<IFormFile>();
            var file = formFile.Object;

            // Act
            var result = controller.CreatePicture(file);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Error", redirectToActionResult.ActionName);
        }

        [Fact]
        public async void CreatePictureReturnsARedirectAndAddPicture()
        {
            // Arrange
            var mock = new Mock<IPictureService>();
            var controller = new HomeController(_logger, mock.Object);

            var formFile = new Mock<IFormFile>();
            var PhysicalFile = new FileInfo(@"C:/Project/net/TimeTravelAgency/TimeTravelAgency/wwwroot/images/Index/welcome.jpg");
            var memory = new MemoryStream();
            var writer = new StreamWriter(memory);
            writer.Write(PhysicalFile.OpenRead());
            writer.Flush();
            memory.Position = 0;
            var fileName = PhysicalFile.Name;

            formFile.Setup(_ => _.FileName).Returns(fileName);
            formFile.Setup(_ => _.Length).Returns(memory.Length);
            formFile.Setup(_ => _.OpenReadStream()).Returns(memory);
            formFile.Verify();

            var file = formFile.Object;
            Picture picture = new Picture() { ViewName = ViewName.Index };

            // Act
            var result = await controller.CreatePicture(file);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.CreatePicture(It.IsAny<Picture>()), Times.Once);
        }
    }
}
