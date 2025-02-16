using System.Threading.Tasks;
using Moq;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Strategies;
using Taxually.TechnicalTest.Clients;
using Xunit;

namespace Taxually.TechnicalTest.Tests
{
    public class UkVatRegistrationStrategyTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldCallHttpClient_WithCorrectUrlAndRequest()
        {
            // Arrange
            var httpClientMock = new Mock<IHttpClient>();
            var strategy = new UkVatRegistrationStrategy(httpClientMock.Object);
            var request = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "12345",
                Country = "GB"
            };

            // Act
            await strategy.RegisterVatAsync(request);

            // Assert
            httpClientMock.Verify(x => x.PostAsync("https://api.uktax.gov.uk", request), Times.Once);
        }
    }
}
