using Hospital.DTOs;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Services.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<IEnumerable<Patient>> SearchPatientsAsync(string search);
        Task<Patient?> GetPatientByIdAsync(int id);
        Task <Patient> AddPatientAsync (PatientDto patientDTO);
        Task <Patient?> UpdatePatientAsync(int id, PatientDto patientDTO);
        Task <bool> DeletePatientAsync(int id);
    

    }
}
