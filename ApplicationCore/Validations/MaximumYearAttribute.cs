using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validations
{
    public class MaximumYearAttribute: ValidationAttribute
    {
        public int Year { get; }
        public MaximumYearAttribute(int year)
        {
            // the year we want to be the max
            Year = year;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // object? value is the year that user entered
            var year = ((DateTime?)value)?.Year;
            if (year < Year)
            {
                return new ValidationResult(errorMessage: $"Year should not be less than {Year}");
            }
            return ValidationResult.Success;
        }
    }
}
