using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels.Author
{
    public class AuthorFormModel
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
        [BirthDate]
        [Display(Name = "Birthday date")]
        public DateTime BirthDate { get; set; }
        
        // [ImagePath]
        [AllowedExtensions(new[] {".png", ".jpg"})]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        
    }
}
