using System.Collections.Generic;
using TestTask.Domain.Models;

namespace TestTask.Domain.ViewModels
{
    public class LibraryItemViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}