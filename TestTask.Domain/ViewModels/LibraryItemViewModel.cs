using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.ViewModels
{
    public class LibraryItemViewModel
    {
        public BookViewModel Book { get; set; }
        [Display(Name = "Authors")]
        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}