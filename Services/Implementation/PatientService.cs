using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
using Hospital.ObserverPattern.Interface;
using Hospital.Repositories;
using Hospital.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital.Services.Implementation
{
    public class PatientService: IPatientService
    {
      
        private readonly IEnumerable<IPatientObserver> _observers;
        private readonly IPatientRepository _repository;

        public PatientService(IPatientRepository repository, IEnumerable<IPatientObserver> observers)
        {
            _repository = repository;
            _observers = observers;
         
        }

        public async Task<Patient> AddPatientAsync(PatientDto patientDTO)
        {
            var patient = new Patient
            {
                Name = patientDTO.Name,
                Age = patientDTO.Age,
                Disease = patientDTO.Disease

            };
           
            foreach (var observer in _observers)
            {
                observer.Update(patient);
            }
            return await _repository.AddAsync(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
            {
              
                return false;
            }

            return await _repository.DeleteAsync(patient);
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            
           var patient = await _repository.GetByIdAsync(id);

            if (patient == null)
            {
                return (null);
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {   
         var patients = await _repository.GetAllAsync();
            return patients;
        }

       

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {

            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<Patient>();
            }
            var patients = await _repository.SearchPatientsAsync(search);
            return (patients);
        }

       

        public async Task<Patient?> UpdatePatientAsync(int id, PatientDto patientDTO)
        {
            var patient = await _repository.GetByIdAsync(id);
            if (patient == null)
            { 
                return null;
            }

            patient.Name = patientDTO.Name;
            patient.Age = patientDTO.Age;
            patient.Disease = patientDTO.Disease;

            await _repository.UpdateAsync(patient);
            return patient;
        }
    }
}
