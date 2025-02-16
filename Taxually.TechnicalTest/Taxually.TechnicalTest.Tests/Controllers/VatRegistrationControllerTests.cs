using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Strategies;
using Xunit;

namespace Taxually.TechnicalTest.Tests
{
    public class VatRegistrationControllerTests
    {
        [Fact]
        public async Task Post_ShouldReturnOk_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var factoryMock = new Mock<IVatRegistrationStrategyFactory>();
            var strategyMock = new Mock<IVatRegistrationStrategy>();
            factoryMock.Setup(x => x.GetStrategy("GB")).Returns(strategyMock.Object);
            var controller = new VatRegistrationController(factoryMock.Object);
            var request = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "12345",
                Country = "GB"
            };

            // Act
            var result = await controller.Post(request);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            strategyMock.Verify(x => x.RegisterVatAsync(request), Times.Once);
        }

        [Fact]
        public async Task Post_ShouldReturnBadRequest_WhenCountryIsNotSupported()
        {
            // Arrange
            var vatRegistrationRequest = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "12345",
                Country = "XX"
            };

            var mockFactory = new Mock<IVatRegistrationStrategyFactory>();
            mockFactory.Setup(f => f.GetStrategy("XX")).Throws(new NotImplementedException("Country not supported"));

            var controller = new VatRegistrationController(mockFactory.Object);

            // Act
            var result = await controller.Post(vatRegistrationRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Country not supported", badRequestResult.Value);
        }
    }
}
