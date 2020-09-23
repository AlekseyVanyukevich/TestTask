
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Year { get; set; }
        [NotMapped]
        public virtual ICollection<BookAuthor> Authors { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}