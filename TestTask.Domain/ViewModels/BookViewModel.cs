using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Book")]
        [StringLength(30, ErrorMessage = "Maximum length of book name is 20")]
        [Required(ErrorMessage = "Book name is required")]
        public string Name { get; set; }
        [Display(Name = "Year")]
        [Required(ErrorMessage = "Year the book was published is required")]
        [ValidYear(ErrorMessage = "Must be a valid year")]
        public int Year { get; set; }
        [Display(Name = "Authors")]
        public IEnumerable<AuthorViewModel> Authors { get; set; }
    }
}