using Hospital.BillingStratergy.Interface;

namespace Hospital.BillingStratergy.Strategies
{
    public class InsuranceBillingStrategy : IBillingStrategy
    {
        public decimal CalculateBill(decimal amount)
        {
            // Insurance billing strategy: apply a 50% discount
            return 0;
        }
    }
    
    
}
