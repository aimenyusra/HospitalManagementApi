using Hospital.BillingStratergy.Interface;

namespace Hospital.BillingStratergy
{
    public class BillingCalculator
    {
        private readonly IBillingStrategy _billingStrategy;
        
        public BillingCalculator(IBillingStrategy billingStrategy)
        {
            _billingStrategy = billingStrategy;
        }

        public decimal CalculateBill(decimal amount)
        {
            return _billingStrategy.CalculateBill(amount);
        }
    }
}
