using Hospital.BillingStratergy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        private readonly BillingStrategyFactory _billingStrategyFactory;
        public BillingController(BillingStrategyFactory billingStrategyFactory)
        {
            _billingStrategyFactory = billingStrategyFactory;
        }
        [HttpGet("calculate")]
        public IActionResult CalculateBill([FromQuery] decimal amount, [FromQuery] string strategyType)
        {
            var billingStrategy = _billingStrategyFactory.GetStrategy(strategyType);
            if (billingStrategy == null)
            {
                return BadRequest("Invalid billing strategy type.");
            }
            var billingCalculator = new BillingCalculator(billingStrategy);
            var totalBill = billingCalculator.CalculateBill(amount);
            return Ok(new { TotalBill = totalBill });
        }
    }
}
