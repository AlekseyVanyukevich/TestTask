using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels.Book
{
    public class BookFormModel
    {
        public int Id { get; set; }
        [Display(Name = "Book")]
        [StringLength(30, ErrorMessage = "Maximum length of book name is 20")]
        [Required(ErrorMessage = "Book name is required")]
        public string Name { get; set; }
        [Display(Name = "Year")]
        [Required(ErrorMessage = "Year the book was published is required")]
        [YearNotMoreThanNow(ErrorMessage = "Must be a valid year")]
        public int Year { get; set; }
        [MustHaveOneElement(ErrorMessage = "At least one author is required")]
        [Display(Name = "Authors")]
        public IEnumerable<int> AuthorIds { get; set; }
    }
}