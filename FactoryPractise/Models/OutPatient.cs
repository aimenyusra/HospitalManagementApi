using Hospital.FactoryPractise.Interfaces;

namespace Hospital.FactoryPractise.Models
{
    public class OutPatient:IPatient
    {
        public string GetPatientType()
        {
            return "Out Patient";
        }
    }
}
