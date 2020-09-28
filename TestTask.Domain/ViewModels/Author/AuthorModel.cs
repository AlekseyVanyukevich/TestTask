using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;
using TestTask.Domain.ViewModels.Book;

namespace TestTask.Domain.ViewModels.Author
{
    public class AuthorModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Surname")]
        public string Surname { get; set; }
        
        [Display(Name = "Birthday date")]
        public DateTime BirthDate { get; set; }
        
        // [Display(Name = "Image")]
        // public string Image { get; set; }
        
        [Display(Name = "Books")]
        public IEnumerable<BookModel> Books { get; set; }
    }
}