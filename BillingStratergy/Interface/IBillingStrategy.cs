namespace Hospital.BillingStratergy.Interface
{
    public interface IBillingStrategy
    {
        decimal CalculateBill(decimal amount);
    }
}
