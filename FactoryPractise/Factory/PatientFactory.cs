using Hospital.FactoryPractise.Interfaces;
using Hospital.FactoryPractise.Models;

namespace Hospital.FactoryPractise.Factory
{
    public class PatientFactory
    {
        public IPatient CreatePatient(string type)
        {
            if (type.ToLower() == "outpatient")
            {
                return new OutPatient();
            }
            if (type.ToLower() == "inpatient")
            {
                return new InPatient();
            }
            if (type.ToLower() == "emergency")
            {
                return new EmergencyPatient();
            }
            else
            {
                throw new ArgumentException("Invalid patient type");
            }
        }
    }
}
