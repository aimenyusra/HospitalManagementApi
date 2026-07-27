using Hospital.DTOs;
using Hospital.Models;
using Hospital.Services.Interfaces;

namespace Hospital.Decorators
{
    public class LoggingPatientServiceDecorator : IPatientService
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<LoggingPatientServiceDecorator> _logger;
        public LoggingPatientServiceDecorator(IPatientService patientService, ILogger<LoggingPatientServiceDecorator> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }
        public async Task<Patient> AddPatientAsync(PatientDto patientDTO)
        {
            _logger.LogInformation("Adding a new patient.");
            var result = await _patientService.AddPatientAsync(patientDTO);
            _logger.LogInformation("Patient added successfully.");
            return result;
        }
        public async Task<bool> DeletePatientAsync(int id)
        {
            _logger.LogInformation("Deleting patient with ID: {id}", id);
            var result = await _patientService.DeletePatientAsync(id);
            if (result)
            {
                _logger.LogInformation("Patient deleted successfully.");
            }
            else
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
            }
            return result;
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving patient with ID: {id}", id);
            var result = await _patientService.GetPatientByIdAsync(id);
            if (result != null)
            {
                _logger.LogInformation("Patient retrieved successfully.");
            }
            else
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
            }
            return result;
        }
        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            _logger.LogInformation("Retrieving all patients.");
            var result = await _patientService.GetPatientsAsync();
            _logger.LogInformation("Patients retrieved successfully.");
            return result;
        }
        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {
            _logger.LogInformation("Searching patients with search term: {search}", search);
            var result = await _patientService.SearchPatientsAsync(search);
            _logger.LogInformation("Patients search completed.");
            return result;
        }
        public async Task<Patient?> UpdatePatientAsync(int id, PatientDto patientDTO)
        {
            _logger.LogInformation("Updating patient with ID: {id}", id);
            var result = await _patientService.UpdatePatientAsync(id, patientDTO);
            if (result != null)
            {
                _logger.LogInformation("Patient updated successfully.");
            }
            else
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
            }
            return result;
        }
      
    }
}
