using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Attributes
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime <= DateTime.Now;
            }

            return false;
        }
    }
}