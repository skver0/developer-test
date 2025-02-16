using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Strategies;

namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        private readonly IVatRegistrationStrategyFactory _strategyFactory;

        public VatRegistrationController(IVatRegistrationStrategyFactory strategyFactory)
        {
            _strategyFactory = strategyFactory;
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            IVatRegistrationStrategy? strategy = null;

            try {
              strategy = _strategyFactory.GetStrategy(request.Country);
            }
            catch (NotImplementedException) {
              return BadRequest("Country not supported");
            }

            // hackery above and below to avoid compiler warning about nullable strategy
            // ¯\_(ツ)_/¯
            if (strategy == null) {
              return BadRequest("Country not supported");
            }

            await strategy.RegisterVatAsync(request);
            return Ok();
        }
    }
}
