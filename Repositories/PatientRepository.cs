using Hospital.Data;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories
{
    public class PatientRepository :GenericRepository<Patient>, IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context): base(context) 
        {
            _context = context;
        }


        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {
            return await _context.Patients
                .Where(p => p.Name.Contains(search) || p.Disease.Contains(search))
                .ToListAsync();
        }

    
    }
}
