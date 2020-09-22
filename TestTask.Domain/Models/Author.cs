using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [Required]
        public string Surname { get; set; }
        public virtual ICollection<BookAuthor> Books { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Surname}";
        }
    }
}