using System;

namespace Taxually.TechnicalTest.Strategies
{
    public class VatRegistrationStrategyFactory : IVatRegistrationStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public VatRegistrationStrategyFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IVatRegistrationStrategy? GetStrategy(string country) =>
            country switch
            {
                "GB" => _serviceProvider.GetService<UkVatRegistrationStrategy>(),
                "FR" => _serviceProvider.GetService<FranceVatRegistrationStrategy>(),
                "DE" => _serviceProvider.GetService<GermanyVatRegistrationStrategy>(),
                _ => throw new NotImplementedException($"Country {country} is not supported.")
            };
    }
}
