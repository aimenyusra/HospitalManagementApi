using Hospital.Models;
using Hospital.ObserverPattern.Interface;

namespace Hospital.ObserverPattern.Observers
{
    public class EmailObserver :IPatientObserver
    {
        public void Update(Patient patient)
        {
            Console.WriteLine($"Sending email notification for patient: {patient.Name}");
        }
    }
}
