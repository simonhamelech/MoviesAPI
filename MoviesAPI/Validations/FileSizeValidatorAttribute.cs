using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class FileSizeValidatorAttribute : ValidationAttribute
    {
        private readonly int maxFileSizeInMbs;
        public FileSizeValidatorAttribute(int maxFileSizeInMbs)
        {
            this.maxFileSizeInMbs = maxFileSizeInMbs;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            IFormFile formFile = value as IFormFile;
            if (formFile == null)
                return ValidationResult.Success;
            if (formFile.Length > (1024 * 1024 * maxFileSizeInMbs))
                return new ValidationResult($"File size cannot be bigger than {maxFileSizeInMbs}Mbs");
            return ValidationResult.Success;
        }
    }
}
