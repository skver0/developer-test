using System.Text;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Clients;

namespace Taxually.TechnicalTest.Strategies
{
    public class FranceVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly IQueueClient _queueClient;

        public FranceVatRegistrationStrategy(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        public async Task RegisterVatAsync(VatRegistrationRequest request)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName},{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            await _queueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}
