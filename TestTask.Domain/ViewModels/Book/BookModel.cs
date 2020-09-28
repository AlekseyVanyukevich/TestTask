using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;
using TestTask.Domain.ViewModels.Author;

namespace TestTask.Domain.ViewModels.Book
{
    public class BookModel
    {
        public int Id { get; set; }

        [Display(Name = "Book")]
        public string Name { get; set; }
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "Authors")]
        public IEnumerable<AuthorModel> Authors { get; set; }
    }
}