using Hospital.FactoryPractise.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTypeController : ControllerBase
    {
        private readonly PatientFactory _patientFactory;
        public PatientTypeController(PatientFactory patientFactory)
        {
            _patientFactory = patientFactory;
        }
        [HttpGet("{type}")]
        public IActionResult GetPatient(string type)
        {
            try
            {
                var patient = _patientFactory.CreatePatient(type);
                return Ok(patient);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
