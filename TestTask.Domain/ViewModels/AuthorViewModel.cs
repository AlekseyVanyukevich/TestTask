using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [StringLength(15, ErrorMessage = "Author's name length must be less than 15")]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [StringLength(20, ErrorMessage = "Author's surname length must be less than 20")]
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        
        [Required(ErrorMessage = "Birthday date is required")]
        [DataType(DataType.Date)]
        [BirthDate]
        public DateTime BirthDate { get; set; }
        
        [ImageUrl]
        public string Image { get; set; }
        
        [Display(Name = "Books")]
        public IEnumerable<BookViewModel> Books { get; set; }
    }
}