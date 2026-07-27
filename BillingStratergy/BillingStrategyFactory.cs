using Hospital.BillingStratergy.Interface;

namespace Hospital.BillingStratergy
{
    public class BillingStrategyFactory
    {
        public IBillingStrategy GetStrategy (string PatientType)
        {
            if (PatientType == "Insurance")
            {
                return new Strategies.InsuranceBillingStrategy();
            }
            else if (PatientType == "Normal")
            {
                return new Strategies.NormalBillingStrategy();
            }
            else if (PatientType == "Senior")
            {
                return new Strategies.SeniorCitizenStrategy();
            }
            else
            {
                throw new ArgumentException("Invalid patient type");
            }
        }
    }
}
