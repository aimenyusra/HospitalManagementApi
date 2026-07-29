using Hospital.Data;
using Hospital.Repositories;

namespace Hospital.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        public IPatientRepository Patients { get; }
        public UnitOfWork(HospitalDbContext context, IPatientRepository patientRepository)
        {
            _context = context;
            Patients = patientRepository;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
