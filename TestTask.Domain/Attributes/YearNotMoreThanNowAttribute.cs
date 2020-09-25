using System;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Attributes
{
    public class YearNotMoreThanNowAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is int year))
            {
                return false;
            }

            return DateTime.Now.Year >= year;
        }
    }
}