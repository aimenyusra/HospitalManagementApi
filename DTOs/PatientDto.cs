using Hospital.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DTOs
{
    public class PatientDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Range(1,120)]
        [MinimumAge]
        public int Age { get; set; }
        public string Disease { get; set; } = string.Empty;
    }
}
