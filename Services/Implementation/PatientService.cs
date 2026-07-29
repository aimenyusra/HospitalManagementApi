using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
using Hospital.ObserverPattern.Interface;
using Hospital.Repositories;
using Hospital.Services.Interfaces;
using Hospital.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital.Services.Implementation
{
    public class PatientService: IPatientService    
    {
      
        private readonly IEnumerable<IPatientObserver> _observers;
        private readonly IUnitOfWork _unitOfWork;

        public PatientService(IUnitOfWork unitOfWork, IEnumerable<IPatientObserver> observers)
        {
            _unitOfWork = unitOfWork;
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
            return await _unitOfWork.Patients.AddAsync(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(id);
            if (patient == null)
            {
              
                return false;
            }

            return await _unitOfWork.Patients.DeleteAsync(patient);
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            
           var patient = await _unitOfWork.Patients.GetByIdAsync(id);

            if (patient == null)
            {
                return (null);
            }
            return patient;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {   
         var patients = await _unitOfWork.Patients.GetAllAsync();
            return patients;
        }

       

        public async Task<IEnumerable<Patient>> SearchPatientsAsync(string search)
        {

            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<Patient>();
            }
            var patients = await _unitOfWork.Patients.SearchPatientsAsync(search);
            return (patients);
        }

       

        public async Task<Patient> UpdatePatientAsync(int id, PatientDto patientDTO)
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
