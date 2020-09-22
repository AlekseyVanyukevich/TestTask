using System;
using System.Collections;

namespace TestTask.Mvc.Extensions
{
    public static class EnumerableExtensions
    {
        public static string JoinWithNewLine(this IEnumerable enumerable)
        {
            return string.Join(Environment.NewLine, enumerable);
        }
    }
}