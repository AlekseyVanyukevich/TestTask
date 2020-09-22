using System.Collections.Generic;
using TestTask.Domain.Models;

namespace TestTask.Domain.ViewModels
{
    public class LibraryItemViewModel
    {
        public BookViewModel Book { get; set; }
        public IEnumerable<BookViewModel> Authors { get; set; }
    }
}