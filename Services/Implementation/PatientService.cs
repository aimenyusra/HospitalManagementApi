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
        private readonly ILogger<PatientService> _logger;
        private readonly IMemoryCache _memoryCache;
        public PatientService(HospitalDbContext context, ILogger<PatientService> logger, IMemoryCache memoryCache)
        {
            _context = context;
            _logger = logger;
            _memoryCache = memoryCache;
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
            _memoryCache.Remove("Patient_List");
            _logger.LogInformation("Patient added successfully.");
            return (patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return false;
            }


            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            _memoryCache.Remove($"Patient_{id}");
            _memoryCache.Remove("Patient_List");
            return true;
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            string cacheKey = $"Patient_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out Patient? patient))
            {
                _logger.LogInformation("Patient retrieved from cache");
                return (patient);
            }
            patient = await _context.Patients.FindAsync(id);
            _logger.LogInformation("Retrieving patient with ID: {id}", id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return(null);
            }
            _logger.LogInformation("Patient with ID: {id} retrieved successfully.", id);
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            _memoryCache.Set(cacheKey, patient, cacheOptions);
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            string cacheKey = "Patient_List";
            if (_memoryCache.TryGetValue(cacheKey, out List<Patient>? patients))
            {
               _logger.LogInformation("Retrieving all patients from cache.");
                return patients!;
            }
            patients = await _context.Patients
                .OrderBy(p => p.Name)
                .AsNoTracking()
                .ToListAsync();
            _memoryCache.Set(cacheKey, patients);
            _logger.LogInformation("Retrieving all patients.");
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
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return null;
            }

            patient.Name = patientDTO.Name;
            patient.Age = patientDTO.Age;
            patient.Disease = patientDTO.Disease;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Patient with ID: {id} updated successfully.", id);
            _memoryCache.Remove($"Patient_{id}");
            _memoryCache.Remove("Patient_List");

            return patient;
        }
    }
}
