using Hospital.Models;

namespace Hospital.ObserverPattern.Interface
{
    public interface IPatientObserver
    {
        void Update (Patient patient);
    }
}
