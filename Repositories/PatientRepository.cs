using Hospital.Data;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Repositories
{
    public class PatientRepository :IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task <IEnumerable<Patient>> GetPatientsAsync()
        {
            return await _context.Patients.ToListAsync();
        }
        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {
            return await _context.Patients
                .Where(p => p.Name.Contains(search) || p.Disease.Contains(search))
                .ToListAsync();
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
        public async Task<Patient?> UpdatePatientAsync( Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<bool> DeletePatientAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
