using Hospital.DTOs;
using Hospital.Models;
using Hospital.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital.Decorators
{
    public class CachingPatientServiceDecorator :IPatientService
    {
        private readonly IPatientService _patientService;
        private readonly IMemoryCache _memoryCache;
        public CachingPatientServiceDecorator(IPatientService patientService, IMemoryCache memoryCache)
        {
            _patientService = patientService;
            _memoryCache = memoryCache;
        }
        public async Task<Patient> AddPatientAsync(PatientDto patientDTO)
        {
            var result = await _patientService.AddPatientAsync(patientDTO);
            _memoryCache.Remove("Patient_List");
            return result;
        }
        public async Task<bool> DeletePatientAsync(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (result)
            {
                _memoryCache.Remove($"Patient_{id}");
                _memoryCache.Remove("Patient_List");
            }
            return result;
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            string cacheKey = $"Patient_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out Patient? patient))
            {
                return (patient);
            }
            else
            {
                patient = await _patientService.GetPatientByIdAsync(id);
                if (patient != null)
                {
                    _memoryCache.Set(cacheKey, patient, TimeSpan.FromMinutes(5));
                }
                return (patient);
            }
        }
        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            string cacheKey = "Patient_List";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Patient>? patients))
            {
                return (patients);
            }
            else
            {
                patients = await _patientService.GetPatientsAsync();
                _memoryCache.Set(cacheKey, patients, TimeSpan.FromMinutes(5));
                return (patients);
            }
        }
        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {
            string cacheKey = $"Patient_Search_{search}";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Patient>? patients))
            {
                return (patients);
            }
            else
            {
                patients = await _patientService.SearchPatientsAsync(search);
                _memoryCache.Set(cacheKey, patients, TimeSpan.FromMinutes(5));
                return (patients);
            }
        }
        public async Task<Patient> UpdatePatientAsync(int id, PatientDto patientDTO)
        {
            var result = await _patientService.UpdatePatientAsync(id, patientDTO);
            if (result != null)
            {
                _memoryCache.Remove($"Patient_{id}");
                _memoryCache.Remove("Patient_List");
            }
            return result;
        }
       
    }
}
