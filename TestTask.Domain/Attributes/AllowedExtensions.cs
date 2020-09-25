using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TestTask.Domain.Attributes
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensions(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var failResult = new ValidationResult("This image extension is not allowed!");
            if (!(value is IFormFile file))
            {
                return failResult;
            }

            var extension = Path.GetExtension(file.FileName);
            return _extensions.Contains(extension) ? ValidationResult.Success : failResult;
        }
    }
}