using Hospital.Repositories;

namespace Hospital.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPatientRepository Patients { get; }
        Task <int> SaveChangesAsync();
    }
}
