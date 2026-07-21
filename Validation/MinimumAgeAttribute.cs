using System.ComponentModel.DataAnnotations;

namespace Hospital.Validation
{
    public class MinimumAgeAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int age)
            {
                if (age < 18)
                {
                    return new ValidationResult("Age must be at least 18.");
                }
                else
                {
                    return ValidationResult.Success!;
                }

            }
            else
            {
                return new ValidationResult("Invalid age value.");

            } 
        }
    }
}
