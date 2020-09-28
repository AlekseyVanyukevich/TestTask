using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using TestTask.Domain.Attributes;

namespace TestTask.Domain.ViewModels.Author
{
    public class AuthorFormModel
    {
        private DateTime? dateCreated = null;
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
        [BirthDate(ErrorMessage = "Birthday must be less than the present day")]
        [Display(Name = "Birthday")]
        public DateTime BirthDate
        {
            get => dateCreated ?? DateTime.Now;
            set => dateCreated = value;
        }
        
        // [ImagePath]
        // [AllowedExtensions(new[] {".png", ".jpg"})]
        // [Display(Name = "Image")]
        // public IFormFile Image { get; set; }
        
    }
}
