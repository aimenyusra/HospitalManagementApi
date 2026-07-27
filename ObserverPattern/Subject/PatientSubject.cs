using Hospital.Models;
using Hospital.ObserverPattern.Interface;

namespace Hospital.ObserverPattern.Subject
{
    public class PatientSubject
    {
        private readonly List<IPatientObserver> _observers;
        
        public void Register(IPatientObserver observer)
        {
            _observers.Add(observer);
        }
        public void Notify(Patient patient)
        {
            foreach (var observer in _observers)
            {
                observer.Update(patient);
            }

        } }
}
