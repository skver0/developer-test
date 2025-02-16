using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Moq;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Strategies;
using Taxually.TechnicalTest.Clients;
using Xunit;

namespace Taxually.TechnicalTest.Tests
{
    public class GermanyVatRegistrationStrategyTests
    {
        [Fact]
        public async Task RegisterAsync_ShouldEnqueueXmlFile_WithCorrectPayload()
        {
            // Arrange
            var queueClientMock = new Mock<IQueueClient>();
            var strategy = new GermanyVatRegistrationStrategy(queueClientMock.Object);
            var request = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "12345",
                Country = "DE"
            };

            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            var stringWriter = new System.IO.StringWriter();
            serializer.Serialize(stringWriter, request);
            var expectedXml = stringWriter.ToString();

            // Act
            await strategy.RegisterVatAsync(request);

            // Assert
            queueClientMock.Verify(x => x.EnqueueAsync("vat-registration-xml", It.Is<string>(xml =>
                xml == expectedXml)), Times.Once);
        }
    }
}
