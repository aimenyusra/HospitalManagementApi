using Hospital.DTOs;
using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Services.Interfaces
{
    public interface IPatientService 
    {
        
        Task<IEnumerable<Patient>> SearchPatientsAsync(string search);
        Task<Patient> AddPatientAsync(PatientDto patientdto);
        Task<Patient> UpdatePatientAsync(int id, PatientDto patientdto);
        Task<bool> DeletePatientAsync(int id);
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<IEnumerable<Patient>> GetPatientsAsync();



    }
}
