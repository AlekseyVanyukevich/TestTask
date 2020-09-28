using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestTask.Domain.Attributes
{
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is IEnumerable enumerable)) return false;
            return enumerable.Cast<object?>().Any();
        }
    }
}