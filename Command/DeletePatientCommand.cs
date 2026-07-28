using Hospital.Services.Interfaces;

namespace Hospital.Command
{
    public class DeletePatientCommand : ICommand
    {
        private readonly IPatientService _patientService;
        private readonly int _patientId;

        public DeletePatientCommand(IPatientService patientService, int patientId)
        {
            _patientService = patientService;
            _patientId = patientId;
        }

        public async Task ExecuteAsync()
        {
            await _patientService.DeletePatientAsync(_patientId);
        }
    }
}
