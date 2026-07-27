using Hospital.Models;
using Hospital.ObserverPattern.Interface;

namespace Hospital.ObserverPattern.Observers
{
    public class DoctorNotificationObserver : IPatientObserver
    {
        public void Update(Patient patient)
        {
            Console.WriteLine($"Doctor notification for patient {patient.Name}");
        }
    }
}
