using System.Threading.Tasks;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Clients;

namespace Taxually.TechnicalTest.Strategies
{
    public class UkVatRegistrationStrategy : IVatRegistrationStrategy
    {
        private readonly IHttpClient _httpClient;

        public UkVatRegistrationStrategy(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task RegisterVatAsync(VatRegistrationRequest request)
        {
            await _httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
