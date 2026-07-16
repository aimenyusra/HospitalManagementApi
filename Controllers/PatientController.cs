using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly HospitalDbContext _context;
        public PatientController(HospitalDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(patients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }
        [HttpPost]
        public async Task<IActionResult> AddPatient(PatientDto patientdto)
        {
            var patient = new Patient
            {
                Name = patientdto.Name,
                Age = patientdto.Age,
                Disease = patientdto.Disease

            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(patient);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientDto patientdto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            patient.Name = patientdto.Name;
            patient.Age = patientdto.Age;
            patient.Disease = patientdto.Disease;

            await _context.SaveChangesAsync();
            return Ok(patient);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
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
