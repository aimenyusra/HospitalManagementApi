using Hospital.BillingStratergy.Interface;

namespace Hospital.BillingStratergy.Strategies
{
    public class NormalBillingStrategy : IBillingStrategy
    {
        public decimal CalculateBill(decimal amount)
        {
            // Normal billing strategy: no discount applied
            return amount;
        }
    }
}
