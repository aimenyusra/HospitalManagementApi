using Hospital.Data;
using Hospital.DTOs;
using Hospital.Models;
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
    public class PatientController(HospitalDbContext context, ILogger<PatientController> logger, IMemoryCache memoryCache) : ControllerBase
    {
        private readonly HospitalDbContext _context = context;
        private readonly ILogger<PatientController> _logger = logger;
        private readonly IMemoryCache _memoryCache = memoryCache;

        [HttpGet]
        public async Task<IActionResult> GetPatients()
        {
            string cacheKey = "Patient_List";
            if(_memoryCache.TryGetValue(cacheKey, out List<Patient>? patients))
            {
               return Ok(patients);
            }
             patients = await _context.Patients.OrderBy(p =>p.Name).AsNoTracking().ToListAsync();
            _memoryCache.Set(cacheKey, patients);
            _logger.LogInformation("Retrieving all patients.");

            return Ok(patients);

        }
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return BadRequest("search value is required");
            }
            var patients = await _context.Patients
                           .Where(p =>
                           p.Name.Contains(search)||
                           p.Disease.Contains(search))
                           .ToListAsync();
            return Ok(patients);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            string cacheKey = $"Patient_{id}";
            if (_memoryCache.TryGetValue(cacheKey, out Patient? patient))
            {
                _logger.LogInformation("Patient retrieved from cache");
                return Ok(patient);
            }
             patient = await _context.Patients.FindAsync(id);
            _logger.LogInformation("Retrieving patient with ID: {id}", id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Patient with ID: {id} retrieved successfully.", id);
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

            _memoryCache.Set(cacheKey, patient, cacheOptions);
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
            _memoryCache.Remove("Patient_List");
            _logger.LogInformation("Patient added successfully.");
            return Ok(patient);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, PatientDto patientdto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return NotFound();
            }

            patient.Name = patientdto.Name;
            patient.Age = patientdto.Age;
            patient.Disease = patientdto.Disease;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Patient with ID: {id} updated successfully.", id);
            _memoryCache.Remove($"Patient_{id}");
            _memoryCache.Remove("Patient_List");
            
            return Ok(patient);
        }
        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                _logger.LogWarning("Patient with ID: {id} not found.", id);
                return NotFound();
            }
           
            
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            _memoryCache.Remove($"Patient_{id}");
            _memoryCache.Remove("Patient_List");
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
