using System.ComponentModel.DataAnnotations;

namespace MockUsers.Validation
{
    public class GenderValidation : ValidationAttribute
    {
        public string[] AllowableValues { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string gender)
            {
                if (gender.Equals("female", StringComparison.OrdinalIgnoreCase) ||
                    gender.Equals("male", StringComparison.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Gender must be 'female' or 'male'.");
        }
    }
}
