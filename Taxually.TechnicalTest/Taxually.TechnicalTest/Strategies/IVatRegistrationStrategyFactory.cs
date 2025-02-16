namespace Taxually.TechnicalTest.Strategies
{
    public interface IVatRegistrationStrategyFactory
    {
        IVatRegistrationStrategy? GetStrategy(string country);
    }
}
