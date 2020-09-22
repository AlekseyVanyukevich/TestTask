using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask.Console.Extensions
{
    public static class EnumerableExtensions
    {
        public static string ToMenu<T>(this IEnumerable<T> enumerable, Func<T, string> fn)
        {
            return string.Join(Environment.NewLine, enumerable.Select(fn));
        }
    }
}