using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Maximum length of book name is 20")]
        [Required(ErrorMessage = "Book name is required")]
        [Display(Name = "Book")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Year the book was published is required")]
        [ValidYear(ErrorMessage = "Must be valid year")]
        [Display(Name = "Year")]
        public int Year { get; set; }
        [Display(Name = "Authors")]
        [MustHaveOneElement(ErrorMessage = "At least one author is required")]
        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}