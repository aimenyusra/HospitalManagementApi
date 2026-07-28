using Hospital.Models;
using Hospital.ObserverPattern.Interface;

namespace Hospital.ObserverPattern.Observers
{
    public class SmsObserver : IPatientObserver
    {
        public void Update(Patient patient)
        {
            Console.WriteLine($"Sending SMS notification for patient: {patient.Name}");
        }
    
    }
}
