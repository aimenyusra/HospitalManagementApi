using Hospital.FactoryPractise.Interfaces;

namespace Hospital.FactoryPractise.Models
{
    public class InPatient: IPatient
    {
        public string GetPatientType()
        {
            return "In Patient";
        }
    }
    
}
