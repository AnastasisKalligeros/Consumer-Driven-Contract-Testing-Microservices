using Microsoft.AspNetCore.Mvc;

namespace ProviderService.Controllers
{
    [ApiController]
    [Route("discount")]
    public class DiscountController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDiscount()
        {
            var discountResponse = new
            {
                customerRating = 4.5,
                amountToDiscount = 0.45
            };

            return Ok(discountResponse);
        }
    }
}
