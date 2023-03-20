using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Shared.CustomAttributes;



public class StartTimeLimiter: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        value = (DateTime)value;
        // This assumes inclusivity, i.e. exactly six years ago is okay
        if (DateTime.Today.CompareTo(value) <= 0 && DateTime.Today.AddYears(1).CompareTo(value) > 0)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Start Date must be within the year.");
        }
    }
}

public class EndTimeLimiter: ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        value = (DateTime)value;
        // This assumes inclusivity, i.e. exactly six years ago is okay
        if (DateTime.Today.AddDays(1).CompareTo(value) <= 0 && DateTime.Today.AddYears(1).CompareTo(value) > 0)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("End Date must be between and next year.");
        }
    }
}

