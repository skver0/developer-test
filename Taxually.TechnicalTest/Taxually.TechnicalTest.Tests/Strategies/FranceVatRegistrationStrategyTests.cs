using System.Text;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Strategies;
using Taxually.TechnicalTest.Clients;
using Xunit;
using Moq;

namespace Taxually.TechnicalTest.Tests
{
    public class FranceVatRegistrationStrategyTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldEnqueueCsvFile_WithCorrectPayload()
        {
            // Arrange
            var queueClientMock = new Mock<IQueueClient>();
            var strategy = new FranceVatRegistrationStrategy(queueClientMock.Object);
            
            var request = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "12345",
                Country = "FR"
            };

            var expectedCsv = Encoding.UTF8.GetBytes("CompanyName,CompanyId\nTestCompany,12345\n");

            // Act
            await strategy.RegisterVatAsync(request);

            // Assert
            queueClientMock.Verify(x => x.EnqueueAsync("vat-registration-csv", It.Is<byte[]>(csv =>
                Encoding.UTF8.GetString(csv) == Encoding.UTF8.GetString(expectedCsv))), Times.Once);
        }
    }
}
