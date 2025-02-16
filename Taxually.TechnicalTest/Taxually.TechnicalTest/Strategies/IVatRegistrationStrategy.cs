using System.Threading.Tasks;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Strategies
{
    public interface IVatRegistrationStrategy
    {
        Task RegisterVatAsync(VatRegistrationRequest request);
    }
}