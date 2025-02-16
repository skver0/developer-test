using System;
using Microsoft.Extensions.DependencyInjection;
using Taxually.TechnicalTest.Strategies;
using Taxually.TechnicalTest.Clients;
using Xunit;

namespace Taxually.TechnicalTest.Tests
{
    public class VatRegistrationStrategyFactoryTests
    {
        [Fact]
        public void GetStrategy_ShouldReturnCorrectStrategy_ForSupportedCountry()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<IHttpClient, TaxuallyHttpClient>();
            services.AddTransient<IQueueClient, TaxuallyQueueClient>();
            services.AddTransient<UkVatRegistrationStrategy>();
            services.AddTransient<FranceVatRegistrationStrategy>();
            services.AddTransient<GermanyVatRegistrationStrategy>();
            var serviceProvider = services.BuildServiceProvider();
            var factory = new VatRegistrationStrategyFactory(serviceProvider);

            // Act
            var germanyStrategy = factory.GetStrategy("DE");
            var franceStrategy = factory.GetStrategy("FR");
            var ukStrategy = factory.GetStrategy("GB");

            // Assert
            Assert.IsType<GermanyVatRegistrationStrategy>(germanyStrategy);
            Assert.IsType<FranceVatRegistrationStrategy>(franceStrategy);
            Assert.IsType<UkVatRegistrationStrategy>(ukStrategy);
        }

        [Fact]
        public void GetStrategy_ShouldThrowException_ForUnsupportedCountry()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddTransient<UkVatRegistrationStrategy>();
            services.AddTransient<FranceVatRegistrationStrategy>();
            services.AddTransient<GermanyVatRegistrationStrategy>();
            var serviceProvider = services.BuildServiceProvider();
            var factory = new VatRegistrationStrategyFactory(serviceProvider);

            // Act
            Action act = () => factory.GetStrategy("ES");

            // Assert
            Assert.Throws<NotImplementedException>(act);
        }
    }
}
