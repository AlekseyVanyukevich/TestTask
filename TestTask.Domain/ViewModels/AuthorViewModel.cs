using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "Maximum length of author surname is 20")]
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        
        [Display(Name = "Books")]
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}