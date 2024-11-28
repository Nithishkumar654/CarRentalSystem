using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using CarRental_System.Data;

namespace CarRental_System.Filters
{
    public class ValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dt)
            {
                if (dt > DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Car must be manufactured in the past.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Email is required.");
            }

            string email = value.ToString();
            var dbContext = validationContext.GetService(typeof(AppDbContext)) as AppDbContext;
            if (dbContext == null)
            {
                throw new InvalidOperationException("DbContext is not available for dependency injection.");
            }

            if (dbContext.Users.Any(u => u.Email == email))
            {
                return new ValidationResult("Email Address is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
