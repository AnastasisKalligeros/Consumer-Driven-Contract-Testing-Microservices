using ProviderService.Models;

namespace ProviderService.Services
{
    public class DiscountService
    {
        public DiscountResponse CalculateDiscount(double customerRating)
        {
            // Example calculation logic
            double amountToDiscount = customerRating * 0.1; // Adjust as needed
            return new DiscountResponse
            {
                CustomerRating = customerRating,
                AmountToDiscount = amountToDiscount
            };
        }
    }
}
