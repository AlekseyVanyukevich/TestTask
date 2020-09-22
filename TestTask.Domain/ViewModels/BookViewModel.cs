using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        [Display(Name = "Book")]
        public string Name { get; set; }
        [Required]
        [ValidYear]
        [Display(Name = "Year")]
        public int Year { get; set; }
    }
}