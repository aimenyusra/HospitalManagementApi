using Hospital.DTOs;
using Hospital.Models;

namespace Hospital.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<IEnumerable<Patient>> SearchPatientsAsync(string search);
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient?> UpdatePatientAsync( Patient patient);
        Task<bool> DeletePatientAsync(Patient patient);
    }
}
