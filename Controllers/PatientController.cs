using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
using Hospital.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Hospital.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
          var patients = await _patientService.GetPatientsAsync();
            return Ok(patients);

        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string search)
        {
            var patients = await _patientService.SearchPatientsAsync(search);
            return Ok(patients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
           var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patientdto)
        {
           var patient = await _patientService.AddPatientAsync(patientdto);
            return Ok(patient);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientDto patientdto)
        {
         var patient = await _patientService.UpdatePatientAsync(id, patientdto);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _patientService.DeletePatientAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
           var userName = User.Identity?.Name;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            return Ok(new
            {
                userName,
                role,
                userId
            });
        }
        [HttpGet("test-error")]
        public async Task <IActionResult> TestError()
        {
            throw new Exception("Testing Middleware");
        }
    }
}
