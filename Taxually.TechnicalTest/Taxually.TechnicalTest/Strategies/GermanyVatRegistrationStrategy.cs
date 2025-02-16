using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Clients;

namespace Taxually.TechnicalTest.Strategies
{
    public class GermanyVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly IQueueClient _queueClient;

        public GermanyVatRegistrationStrategy(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task RegisterVatAsync(VatRegistrationRequest request)
        {
            using var stringWriter = new StringWriter();
            var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
            serializer.Serialize(stringWriter, request);
            var xml = stringWriter.ToString();
            await _queueClient.EnqueueAsync("vat-registration-xml", xml);
        }
    }
}
