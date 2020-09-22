using System;
using System.Threading.Tasks;

namespace TestTask.Console.Models
{
    public class MenuItem
    {
        public int Index { get; set; }
        public Func<Task> ActionAsync { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Index}. {Title}";
        }
    }
}