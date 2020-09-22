using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }
    }
}