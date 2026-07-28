using Hospital.BillingStratergy.Interface;

namespace Hospital.BillingStratergy.Strategies
{
    public class SeniorCitizenStrategy : IBillingStrategy
    {
        public decimal CalculateBill(decimal amount)
        {
            // Senior citizen billing strategy: apply a 20% discount
            return amount - (amount * 0.2m);
        }
    }
    
    
}
