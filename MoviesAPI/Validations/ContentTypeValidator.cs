using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly string[] ValidContentTypes;
        private readonly string[] imageContentTypes = new string[] { "image/jpeg", "image/png", "image/gif" };

        public ContentTypeValidator(string[] validContentTypes)
        {
            this.ValidContentTypes = validContentTypes;
        }
        public ContentTypeValidator(ContentTypeGroup contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentTypeGroup.Image:
                    ValidContentTypes = imageContentTypes;
                    break;
                default:
                    break;
            }
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((value == null) || !(value is IFormFile formFile))
                return ValidationResult.Success;
            if (!ValidContentTypes.Contains(formFile.ContentType))
                return new ValidationResult($"File type {formFile.ContentType} not supported. Please use on of {string.Join(",", ValidContentTypes)}");
            return ValidationResult.Success;
        }
    }
    public enum ContentTypeGroup
    {
        Image
    }
}
