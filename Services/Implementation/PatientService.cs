using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
using Hospital.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital.Services.Implementation
{
    public class PatientService: IPatientService
    {
        private readonly HospitalDbContext _context;
       
       
        public PatientService(HospitalDbContext context)
        {
            _context = context;
         
        }

        public async Task<Patient> AddPatientAsync(PatientDto patientDTO)
        {
            var patient = new Patient
            {
                Name = patientDTO.Name,
                Age = patientDTO.Age,
                Disease = patientDTO.Disease

            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
    
            return (patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
              
                return false;
            }


            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            
           var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return (null);
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {   
         var patients = await _context.Patients
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .ToListAsync();
            return patients;
        }

       

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {

            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<Patient>();
            }
            var patients = await _context.Patients
                           .Where(p =>
                           p.Name.Contains(search) ||
                           p.Disease.Contains(search))
                           .ToListAsync();
            return (patients);
        }

       

        public async Task<Patient?> UpdatePatientAsync(int id, PatientDto patientDTO)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            { 
                return null;
            }

            patient.Name = patientDTO.Name;
            patient.Age = patientDTO.Age;
            patient.Disease = patientDTO.Disease;

            await _context.SaveChangesAsync();
          

            return patient;
        }
    }
}
