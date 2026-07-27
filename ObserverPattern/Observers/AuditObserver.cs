using Hospital.Models;
using Hospital.ObserverPattern.Interface;

namespace Hospital.ObserverPattern.Observers
{
    public class AuditObserver : IPatientObserver
    {
        public void Update(Patient patient)
        {
            Console.WriteLine($"Logging audit information for patient: {patient.Name}");
        }
    
    }
}
