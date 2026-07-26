using Hospital.FactoryPractise.Interfaces;

namespace Hospital.FactoryPractise.Models
{
    public class EmergencyPatient : IPatient
    {
        public string GetPatientType()
        {
            return "Emergency Patient";
        }

    }
}
