using Hospital.DTOs;
using Hospital.Models;

namespace Hospital.Repositories
{
    public interface IPatientRepository: IGenericRepository<Patient>
    {

        Task<IEnumerable<Patient>> SearchPatientsAsync(string search);
     
    }
}
